using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int TherapistID { get; set; }

        [Required]
        public int CSID { get; set; }

        [Required]
        public int SessionID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Navigation properties
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("TherapistID")]
        public Therapist Therapist { get; set; }

        [ForeignKey("CSID")]
        public CustomerSupport CustomerSupport { get; set; }
    }
}
