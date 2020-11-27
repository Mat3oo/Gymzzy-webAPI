using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class TrainingEditDTO
    {
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public ICollection<TrainingEditSeriesDTO> Series { get; set; }

        public class TrainingEditSeriesDTO
        {
            public Guid? Id { get; set; }
            [Required]
            public int? Reps { get; set; }
            [Required]
            public float? Weight { get; set; }
            [Required]
            public TrainingEditExerciseDTO Exercise { get; set; }
            public class TrainingEditExerciseDTO
            {
                [Required]
                public string Name { get; set; }
            }
        }

    }
}
