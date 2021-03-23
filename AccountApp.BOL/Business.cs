#region Header

#endregion
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// Model class for Business
/// </summary>
namespace AccountApp.BOL
{
    public class BusinessModel
    {
        [Key]
        public int Id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BusinessNo { get; set; }

        public string BusinessName { get; set; }
        public string HstGstNumber { get; set; }
        public PhoneAttribute Phone { get; set; }
        public EmailAddressAttribute Email { get; set; }
        public string Address { get; set; }

    }
}
