using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class TrainingCreateDTO
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public ICollection<SeriesDTO> Series { get; set; }

        public class SeriesDTO
        {
            [Required]
            public int Reps { get; set; }
            [Required]
            public float Weight { get; set; }
            [Required]
            public ExerciseDTO Exercise { get; set; }
            public class ExerciseDTO
            {
                [Required]
                public string Name { get; set; }
            }
        }

    }
}
