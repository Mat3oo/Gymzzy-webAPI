using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserDetailsEditDTO
    {
        [StringLength(256, ErrorMessage = "Max {0} length is {1} characters")]
        public string Name { get; set; }
        [StringLength(256, ErrorMessage = "Max {0} is {1} characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(256, ErrorMessage = "Max {0} length is {1} characters")]
        public string UserName { get; set; }
        [StringLength(maximumLength: 1, MinimumLength = 1, ErrorMessage = "{0} should be expressed with {1} character")]
        [RegularExpression("[fm]")]
        public string Gender { get; set; }
        [Range(0, 300, ErrorMessage = "{0} should be between {1} - {2}")]
        public float? Height { get; set; }
        [Range(0, 1000, ErrorMessage = "{0} should be between {1} - {2}")]
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
