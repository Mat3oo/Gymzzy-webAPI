﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class UserDetailsEditDTO
    {
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string LastName { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(maximumLength: 1, MinimumLength = 1)]
        [RegularExpression("[fm]")]
        public string Gender { get; set; }
        [Range(0, 300)]
        public float? Height { get; set; }
        [Range(0, 1000)]
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
