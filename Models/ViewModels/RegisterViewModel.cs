using System.ComponentModel.DataAnnotations;

namespace Webapp1.Models.ViewModels
{

     public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Passord må være på minst 6 tegn")]
        public string Password { get; set; }

    }
}