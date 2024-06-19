using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RehabConnect.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        public decimal TotalAmount { get; set; }

        public int ParentID { get; set; }
        [ForeignKey("ParentID")]
        [ValidateNever]
        public ParentDetail ParentDetail { get; set; }  // Changed to ParentDetail

        public DateTime DateIssued { get; set; }
    }
}
