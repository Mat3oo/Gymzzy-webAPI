using System;

namespace GymzzyWebAPI.Models
{
    public class Series
    {
        public Guid Id { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }

        public Guid Training_Id { get; set; }
        public Training Training { get; set; }

        public Guid Exercsise_Id { get; set; }
        public Exercise Exercise { get; set; }
    }
}