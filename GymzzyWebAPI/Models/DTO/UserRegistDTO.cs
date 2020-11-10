using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserRegistDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Pass a valid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(256, ErrorMessage = "Max {0} length is {1} characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }
}
