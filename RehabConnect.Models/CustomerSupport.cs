using System;
using System.Collections.Generic;
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
        public string CSName { get; set; }

        [Required]
        public string CSIC { get; set; }

        [Required]
        public int CSAge { get; set; }

        [Required]
        public string CSSex { get; set; }

        [Required]
        public string CSReligion { get; set; }

        [Required]
        public string CSNationality { get; set; }

        [Required]
        public string CSPhoneNum { get; set; }

        [Required]
        public string CSAddress { get; set; }

        [Required]
        public string CSEmail { get; set; }


        // Navigation properties
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
