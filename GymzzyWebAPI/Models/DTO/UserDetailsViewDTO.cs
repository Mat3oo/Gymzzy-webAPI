using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class UserDetailsViewDTO
    {
        public Guid Id { get; set; }
        [StringLength(256, ErrorMessage = "Max name length is {1} characters")]
        public string Name { get; set; }
        [StringLength(256, ErrorMessage = "Max last name is {1} characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Nick is required")]
        [StringLength(256, ErrorMessage = "Max nick lenth is {1} characters")]
        public string Nick { get; set; }
        [EmailAddress(ErrorMessage = "Pass a valid Email")]
        public string Email { get; set; }
        [MinLength(1, ErrorMessage = "Gender should be expressed with one character")]
        public char? Gender { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
