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

        [Required]
        [Display(Name ="Business No.")]
        public string BusinessNo { get; set; }

        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "Hst Or Gst Number")]
        public string HstGstNumber { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
