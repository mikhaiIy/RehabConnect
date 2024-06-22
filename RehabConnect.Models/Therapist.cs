using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Therapist
    {
        [Key]
        public int TherapistID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string TherapistName { get; set; }

        [Required]
        [DisplayName("NRIC")]
        public string TherapistIC { get; set; }

        [Required]
        [DisplayName("Age")]
        public int TherapistAge { get; set; }

        [Required]
        [DisplayName("Sex")]
        public string TherapistSex { get; set; }

        [Required]
        [DisplayName("Religion")]
        public string TherapistReligion { get; set; }

        [Required]
        [DisplayName("Nationality")]
        public string TherapistNationality { get; set; }

        [Required]
        [DisplayName("Phone No")]
        public string TherapistPhoneNum { get; set; }

        [Required]
        [DisplayName("Address")]
        public string TherapistAddress { get; set; }

        [Required]
        [DisplayName("Email")]
        public string TherapistEmail { get; set; }


        // Navigation properties
        public ICollection<Session>? Sessions { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
