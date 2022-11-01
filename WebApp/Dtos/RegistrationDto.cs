using System.ComponentModel.DataAnnotations;

namespace Wm22App.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }
    }
}