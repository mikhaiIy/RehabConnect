using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RehabConnect.Models;

public class InvoiceItem
{
    [Key]
    public int InvoiceItemId { get; set; }
    
    [Required]
    public string Items { get; set; }

    [Required]
    public string ItemsInformation { get; set; }

    [Required]
    public decimal Cost { get; set; }
    
    [Required]
    public decimal Qty { get; set; }
    
    [Required]
    public decimal Price { get; set; }

    public int InvoiceId { get; set; }
    [ForeignKey("InvoiceId")]
    [ValidateNever]
    public Invoice Invoice { get; set; }
}