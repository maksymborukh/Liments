using System.ComponentModel.DataAnnotations;

namespace Liments.MVC.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorerct password")]
        public string ConfirmPassword { get; set; }
    }
}
