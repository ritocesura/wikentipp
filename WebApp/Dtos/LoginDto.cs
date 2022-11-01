using System.ComponentModel.DataAnnotations;

namespace Wm22App.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email wird benötigt!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Passwort wird benötigt!")]
        public string Password { get; set; }
    }
}