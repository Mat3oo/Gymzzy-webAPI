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
        public ICollection<TrainingCreateDTOExercise> Exercises { get; set; }

        public class TrainingCreateDTOExercise
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public ICollection<TrainingCreateDTOSet> Sets { get; set; }

            public class TrainingCreateDTOSet
            {
                [Required]
                public int? Reps { get; set; }
                [Required]
                public float? Weight { get; set; }
            }
        }

    }
}
