using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class TrainingViewDTO
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public ICollection<TrainingViewSeriesDTO> Series { get; set; }

        public class TrainingViewSeriesDTO
        {
            public Guid Id { get; set; }

            [Required]
            public int? Reps { get; set; }

            [Required]
            public float? Weight { get; set; }

            public bool Record { get; set; } = false;

            [Required]
            public TrainingViewExerciseDTO Exercise { get; set; }

            public class TrainingViewExerciseDTO
            {
                public Guid Id { get; set; }

                [Required]
                public string Name { get; set; }
            }
        }

    }
}
