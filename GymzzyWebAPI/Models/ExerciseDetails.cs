using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class ExerciseDetails
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
    }
}
