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
        public ICollection<TrainingViewDTOExercise> Exercises { get; set; }

        public class TrainingViewDTOExercise
        {
            public Guid Id { get; set; }

            [Required]
            public string Name { get; set; }

            public ICollection<TrainingViewDTOSet> Sets { get; set; }

            public class TrainingViewDTOSet
            {
                public Guid Id { get; set; }

                [Required]
                public int? Reps { get; set; }

                [Required]
                public float? Weight { get; set; }

                public bool Record { get; set; } = false;
            }
        }

    }
}
