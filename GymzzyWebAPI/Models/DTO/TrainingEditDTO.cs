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
        public ICollection<TrainingEditDTOExercise> Exercises { get; set; }

        public class TrainingEditDTOExercise
        {
            public Guid? Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public ICollection<TrainingEditDTOSet> Sets { get; set; }

            public class TrainingEditDTOSet
            {
                public Guid? Id { get; set; }

                [Required]
                public int? Reps { get; set; }
                [Required]
                public float? Weight { get; set; }
            }
        }
    }
}
