using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class PaymentVM
    {
        public int InvoiceId { get; set; }
        public string ParentName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Amount { get; set; }
        public string? Reciept { get; set; }
    }
}
