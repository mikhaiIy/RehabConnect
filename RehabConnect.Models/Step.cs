using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace RehabConnect.Models
{
    
    public class Step
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StepId { get; set; }
        
        [Required] public int RoadmapId { get; set; }
        [ForeignKey("RoadmapId")]
        public Roadmap Roadmap { get; set; }
        [Required]
        public int StepNo { get; set; }
        [Required]
        public string Title { get; set; }
        
        
    }
}
