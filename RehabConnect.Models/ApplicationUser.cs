using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class ApplicationUser : IdentityUser
    {
        public int? TherapistID { get; set; }
        
        [ForeignKey("TherapistID")]
        [ValidateNever]
        public Therapist Therapist { get; set; }

        public int? CSID { get; set; }

        [ForeignKey("CSID")]
        [ValidateNever]
        public CustomerSupport CustomerSupport { get; set; }





    }
}
