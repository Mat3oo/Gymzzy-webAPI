using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class TrainingCreateDTO
    {
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public ICollection<TrainingCreateSeriesDTO> Series { get; set; }

        public class TrainingCreateSeriesDTO
        {
            [Required]
            public int? Reps { get; set; }
            [Required]
            public float? Weight { get; set; }
            [Required]
            public TrainingCreateExerciseDTO Exercise { get; set; }
            public class TrainingCreateExerciseDTO
            {
                [Required]
                public string Name { get; set; }
            }
        }

    }
}
