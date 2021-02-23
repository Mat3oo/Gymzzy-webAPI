using System;

namespace GymzzyWebAPI.Models
{
    public class PersonalRecord
    {
        public Guid Id { get; set; }

        public Guid SetId { get; set; }
        public Set Set { get; set; }
    }
}
