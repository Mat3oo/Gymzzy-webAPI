using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymzzyWebAPI.Models
{
    public class User
    {
        public User()
        {
            Trainings = new HashSet<Training>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public char? Gender { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
