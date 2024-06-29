using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RehabConnect.Models
{
    public class Announcement
    {
        [Key]
        public int AnnouncementID { get; set; }
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
