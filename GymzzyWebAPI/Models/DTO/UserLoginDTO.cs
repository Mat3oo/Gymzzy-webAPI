using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Pass a valid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }
}
