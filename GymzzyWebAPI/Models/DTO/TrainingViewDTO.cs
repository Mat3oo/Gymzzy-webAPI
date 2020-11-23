using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models.DTO
{
    public class TrainingViewDTO
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public ICollection<SeriesDTO> Series { get; set; }

        public class SeriesDTO
        {
            public Guid Id { get; set; }
            [Required]
            public int Reps { get; set; }
            [Required]
            public float Weight { get; set; }
            [Required]
            public ExerciseDTO Exercise { get; set; }

            public class ExerciseDTO
            {
                public Guid Id { get; set; }
                [Required]
                public string Name { get; set; }
            }
        }

    }
}
