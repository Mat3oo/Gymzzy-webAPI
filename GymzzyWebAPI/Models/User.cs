using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string LastName { get; set; }
        public char? Gender { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
