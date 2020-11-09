using System;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class Series
    {
        public Guid Id { get; set; }
        [Required]
        public int Reps { get; set; }
        [Required]
        public float Weight { get; set; }

        [Required]
        public Guid TrainingId { get; set; }
        public Training Training { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}