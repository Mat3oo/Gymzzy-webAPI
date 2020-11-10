using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserDetailsViewDTO
    {
        public Guid Id { get; set; }
        [StringLength(256, ErrorMessage = "Max {0} length is {1} characters")]
        public string Name { get; set; }
        [StringLength(256, ErrorMessage = "Max {0} is {1} characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(256, ErrorMessage = "Max {0} length is {1} characters")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Pass a valid email format")]
        public string Email { get; set; }
        [MinLength(1, ErrorMessage = "{0} should be expressed with {1} character")]
        public char? Gender { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
