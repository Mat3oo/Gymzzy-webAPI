using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class Set
    {
        public Guid Id { get; set; }
        [Required]
        public int Reps { get; set; }
        [Required]
        public float Weight { get; set; }

        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public PersonalRecord PersonalRecord { get; set; }
    }
}
