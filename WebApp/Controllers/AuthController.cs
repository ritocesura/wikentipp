using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wm22App.Dtos;
using Wm22App.Models;
using Wm22App.Services;

namespace Wm22App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly int NumberOfUserProfiles = 30;
        private UserManager<User> UserManager { get; }
        private IJwtAuthService JwtAuthService { get; }

        public AuthController(UserManager<User> userManager, IJwtAuthService jwtAuthService)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            JwtAuthService = jwtAuthService ?? throw new ArgumentNullException(nameof(jwtAuthService));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await UserManager.GetRolesAsync(user);
                var role = roles?.Count > 0 ? roles[0] : "Anonymous";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var jwtResult = JwtAuthService.GenerateTokens(user.UserName, claims, DateTime.Now);

                return new OkObjectResult(new LoginResult()
                {
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                    Username = user.UserName,
                    Role = role
                });
            }
            return new BadRequestObjectResult($"Benutzer {model.UserName} konnte nicht gefunden werden");
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            // optionally "revoke" JWT token on the server side --> add the current token to a block-list
            // https://github.com/auth0/node-jsonwebtoken/issues/375
            var userName = User.Identity?.Name;
            JwtAuthService.RemoveRefreshTokenByUserName(userName);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new BadRequestObjectResult("Passwörter sind nicht gleich");
            }

            if (await UserManager.FindByNameAsync(model.UserName) is not null)
            {
                return new BadRequestObjectResult("Benutzername bereits vergeben");
            }

            if (await UserManager.FindByNameAsync(model.Email) is not null)
            {
                return new BadRequestObjectResult("Email bereits vergeben");
            }

            Random rng = new Random();
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                UserIcon = $"cat{rng.Next(1, NumberOfUserProfiles + 1)}.jpg"
            };
            var userCreated = await UserManager.CreateAsync(user, model.Password);
            if (!userCreated.Succeeded)
            {
                return new BadRequestObjectResult(string.Join(", ", userCreated.Errors.Select(error => $"{error.Code} {error.Description}")));
            }

            userCreated = await UserManager.AddToRoleAsync(user, "Tippexperte");
            if (!userCreated.Succeeded)
            {
                return new BadRequestObjectResult(string.Join(", ", userCreated.Errors.Select(error => $"{error.Code} {error.Description}")));
            }

            return new OkObjectResult(user);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity?.Name;

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = JwtAuthService.Refresh(request.RefreshToken, accessToken, DateTime.Now);

                return Ok(new LoginResult
                {
                    Username = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }
    }
}