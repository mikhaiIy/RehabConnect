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
        public int InvoiceId { get; set; }
        
        [Required]
        public DateOnly DateIssued { get; set; }
        [Required]
        public DateOnly DueDate { get; set; }
        
        [Required]
        public string PICNames { get; set; }
        
        [Required]
        public string ShortNote { get; set; }
        
        [Required]
        public string LongNote { get; set; }
        
        [Required]
        public decimal Subtotal { get; set; }
        
        [Required]
        public decimal Discount { get; set; }
        
        [Required]
        public decimal Total { get; set; }

        [Required]

        public int ParentID { get; set; }
        [ForeignKey("ParentID")]
        [ValidateNever]
        public ParentDetail ParentDetail { get; set; }  // Changed to ParentDetail       

    }
    //[Required]
    //public string ParentNames { get; set; }
    //[Required]
    //public string Address1 { get; set; }
    //[Required]
    //public string Address2 { get; set; }
    //[Required]
    //public string PhoneNum { get; set; }
    //[Required]
    //public string Email { get; set; }
}
