using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountApp.BOL
{
    public class Business
    {
        [Key]
        public int Id { get; set; }
        public string BusinessNo { get; set; }

        public string BusinessName { get; set; }
        public string HstGstNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
