using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        [Required]
        public int BillingID { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        // Navigation properties
        [ForeignKey("BillingID")]
        public Billing Billing { get; set; }
    }
}
