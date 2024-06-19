using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models
{
    public class Billing
    {
        [Key]
        public int BillingID { get; set; }

        public int ParentID { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        // Navigation properties
        [ForeignKey("ParentID")]
        public ParentDetail ParentDetail { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
