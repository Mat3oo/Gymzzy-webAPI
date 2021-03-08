using System;
using System.Collections.Generic;

namespace GymzzyWebAPI.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }

        public Guid TrainingId { get; set; }
        public Training Training { get; set; }

        public Guid? ExerciseDetailsId { get; set; }
        public ExerciseDetails ExerciseDetails { get; set; }

        public ICollection<Set> Sets { get; set; } = new HashSet<Set>();
    }
}
