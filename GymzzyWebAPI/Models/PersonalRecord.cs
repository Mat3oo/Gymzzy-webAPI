using System;

namespace GymzzyWebAPI.Models
{
    public class PersonalRecord
    {
        public Guid Id { get; set; }

        public Guid SeriesId { get; set; }
        public Series Series { get; set; }
    }
}
