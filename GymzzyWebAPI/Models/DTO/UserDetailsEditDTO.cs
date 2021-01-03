using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserDetailsEditDTO
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(maximumLength: 1, MinimumLength = 1)]
        [RegularExpression("[fm]")]
        public string Gender { get; set; }

        [Required]
        [Range(0, 300)]
        public float? Height { get; set; }

        [Required]
        [Range(0, 1000)]
        public float? Weight { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }
    }
}
