using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int SessionID { get; set; }
        [ForeignKey("SessionID")]
        [ValidateNever]
        public Program Program { get; set; }
        public int ParentID { get; set; }
        [ForeignKey("ParentID")]
        [ValidateNever]
        public Parent Parent { get; set; }
        //public int ApplicationUserId { get; set; }
        //[ForeignKey("ApplicationUserId")]
        //[ValidateNever]
        //public ApplicationUser ApplicationUser { get; set; }

    }
}
