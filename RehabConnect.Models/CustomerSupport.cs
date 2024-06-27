using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class CustomerSupport
    {
        [Key]
        public int CSID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string CSName { get; set; }

        [Required]
        [DisplayName("NRIC")]
        public string CSIC { get; set; }

        [Required]
        [DisplayName("Age")]
        public int CSAge { get; set; }

        [Required]
        [DisplayName("Sex")]
        public string CSSex { get; set; }

        [Required]
        [DisplayName("Religion")]
        public string CSReligion { get; set; }

        [Required]
        [DisplayName("Nationality")]
        public string CSNationality { get; set; }

        [Required]
        [DisplayName("Phone No")]
        public string CSPhoneNum { get; set; }

        [Required]
        [DisplayName("Address")]
        public string CSAddress { get; set; }

        [Required]
        [DisplayName("Email")]
        public string CSEmail { get; set; }



        // // Navigation properties
        // public ICollection<Session>? Sessions { get; set; }
        // public ICollection<Report>? Reports { get; set; }
    }
}
