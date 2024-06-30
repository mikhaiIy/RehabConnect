using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }

        [Required]
        public DateOnly Date { get; set; }
        
        [Required]
        [DisplayName("Session Start")]
        public TimeOnly StartTime { get; set; }
        
        [Required]
        [DisplayName("Session End")]
        public TimeOnly EndDTime { get; set; }
        
        [Required]
        public int Capacity { get; set; }
        
        [Required]
        public int Registered { get; set; }

        public int? ProgramID { get; set; }

        [ForeignKey("ProgramID")]
        [ValidateNever]
        public Program? Program { get; set; }

    }
}
