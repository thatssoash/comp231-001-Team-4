using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountApp.BOL
{
    public class CustomForm
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }

        [Display(Name ="Form Name")]
        public string FormName { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
}
