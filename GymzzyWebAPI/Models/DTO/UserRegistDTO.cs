using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserRegistDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
