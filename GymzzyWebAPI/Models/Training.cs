using System;
using System.Collections;
using System.Collections.Generic;

namespace GymzzyWebAPI.Models
{
    public class Training
    {
        public Training()
        {
            Series = new HashSet<Series>();
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid User_Id { get; set; }
        public User User { get; set; }

        public ICollection<Series> Series{ get; set; }
    }
}