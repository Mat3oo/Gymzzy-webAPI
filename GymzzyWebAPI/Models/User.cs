using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(256, ErrorMessage = "Max {0} length is {1} characters")]
        public string Name { get; set; }
        [StringLength(256, ErrorMessage = "Max {0} is {1} characters")]
        public string LastName { get; set; }
        public char? Gender { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
