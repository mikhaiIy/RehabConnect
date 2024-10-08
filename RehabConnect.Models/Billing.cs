﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RehabConnect.Models
{
    public class Billing
    {
        [Key]
        public int BillingID { get; set; }

        public int InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]
        [ValidateNever]
        public Invoice Invoice { get; set; }
        [Required]
        [ValidateNever]
        [DisplayName("Upload Reciept")]
        public string? Reciept { get; set; } // For storing the Reciept
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public bool ConfirmStatus { get; set; }
    }
}
