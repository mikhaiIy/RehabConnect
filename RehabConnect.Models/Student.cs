using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RehabConnect.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        public string ChildName { get; set; }
        [Required]
        public string ChildIC { get; set; }
        [Required]
        public int ChildAge { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ChildDOB { get; set; }

        public string? ChildPassportNo { get; set; }
        [Required]
        public string ChildNationality { get; set; }
        [Required]
        public string ChildRace { get; set; }
        [Required]
        public string ChildBirthPlace { get; set; }
        [Required]
        [DisplayName("Gender: Male/Female")]
        public string ChildSex { get; set; }
        [Required]
        public string ChildAddress { get; set; }
        [Required]
        public string ChildPostcode { get; set; }
        [Required]
        public string ChildCity { get; set; }
        [Required]
        public string ChildCountry { get; set; }


        [Required]
        public string Pediatricians { get; set; }
        public string? HospReccommendation { get; set; }
        public string? DeadlineDiagnose { get; set; }
        public string? DiagnosisOrCondition { get; set; }
        [DisplayName("Place of Occupational Theraphy Unit")]
        public string? OccupationalTheraphyPlace { get; set; }
        [DisplayName("Place of Speech Theory Unit")]
        public string? SpeechTheoryPlace { get; set; }
        [DisplayName("Place of Others Unit")]
        public string? OthersUnitPlace { get; set; }


        public string? UserId { get; set; }


        // Foreign key for Therapist
        public int? TherapistID { get; set; }

        // Navigation property for Therapist
        [ForeignKey("TherapistID")]
        public Therapist? Therapist { get; set; }

        // Navigation properties

        public ICollection<Session>? Sessions { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
