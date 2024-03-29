﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class Training
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
    }
}
