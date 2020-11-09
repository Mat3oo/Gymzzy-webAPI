using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class User
    {
        public User()
        {
            Trainings = new HashSet<Training>();
        }

        public Guid Id { get; set; }
        [StringLength(256, ErrorMessage = "Max name length is {1} characters")]
        public string Name { get; set; }
        [StringLength(256, ErrorMessage = "Max last name is {1} characters")]
        public string LastName { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "Max nick lenth is {1} characters")]
        public string Nick { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Pass a valid email format")]
        public string Email { get; set; }
        [MinLength(1, ErrorMessage = "Gender should be expressed with one character")]
        public char? Gender { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordSalt { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
