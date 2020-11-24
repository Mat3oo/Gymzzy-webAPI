using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class Exercise
    {
        public Exercise()
        {
            Series = new HashSet<Series>();
        }

        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Series> Series { get; set; }
    }
}