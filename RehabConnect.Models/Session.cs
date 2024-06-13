using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int TherapistID { get; set; }

        [Required]
        public int CSID { get; set; }

        [Required]
        public int ProgramID { get; set; }

        [Required]
        public int ScheduleID { get; set; }

        [Required]
        public string Status { get; set; }

        // Navigation properties
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("TherapistID")]
        public Therapist Therapist { get; set; }

        [ForeignKey("CSID")]
        public CustomerSupport CustomerSupport { get; set; }

        [ForeignKey("ProgramID")]
        public Program Program { get; set; }

        [ForeignKey("ScheduleID")]
        public Schedule Schedule { get; set; }
    }
}
