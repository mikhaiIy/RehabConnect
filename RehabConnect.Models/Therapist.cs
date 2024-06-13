using System;
using System.Collections.Generic;
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
        public string TherapistName { get; set; }

        [Required]
        public string TherapistIC { get; set; }

        [Required]
        public int TherapistAge { get; set; }

        [Required]
        public string TherapistSex { get; set; }

        [Required]
        public string TherapistReligion { get; set; }

        [Required]
        public string TherapistNationality { get; set; }

        [Required]
        public string TherapistPhoneNum { get; set; }

        [Required]
        public string TherapistAddress { get; set; }

        [Required]
        public string TherapistEmail { get; set; }


        // Navigation properties
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
