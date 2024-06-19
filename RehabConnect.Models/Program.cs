using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Program
    {
        [Key]
        public int SessionID { get; set; }

        [Required]
        public string ProgramName { get; set; }

        [Required]
        public int NumOfSession { get; set; }
        
        // public int? NormalPrice { get; set; }
        public int? PriceWeekday { get; set; }
        public int? PriceWeekend { get; set; }


        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public Step Step { get; set; }

        // Navigation properties
        public ICollection<Session> Sessions { get; set; }
    }
}
