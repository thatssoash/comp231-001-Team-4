using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApp.Models
{
    public class AppUser
    {
        public string SIN { get; set; }

        public string Address { get; set; }

        public int BusinessCount { get; set; }

        public DateTime DateEdit { get; set; } = DateTime.Now;
    }
}
