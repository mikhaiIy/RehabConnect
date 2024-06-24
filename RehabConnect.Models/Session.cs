using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public int StudentProgramId { get; set; }

        [Required]
        public int ScheduleID { get; set; }
        

        // Navigation properties
        [ForeignKey("StudentProgramId")]
        [ValidateNever]
        public StudentProgram? StudentProgram { get; set; }

        [ForeignKey("ScheduleID")]
        [ValidateNever]
        public Schedule? Schedule { get; set; }
    }
}
