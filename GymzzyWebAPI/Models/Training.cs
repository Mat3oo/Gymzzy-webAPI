using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymzzyWebAPI.Models
{
    public class Training
    {
        public Training()
        {
            Series = new HashSet<Series>();
        }

        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public ICollection<Series> Series { get; set; }
    }
}