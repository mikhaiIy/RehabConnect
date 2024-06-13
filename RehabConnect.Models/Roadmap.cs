using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RehabConnect.Models
{
    public class Roadmap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoadmapId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
    
}

