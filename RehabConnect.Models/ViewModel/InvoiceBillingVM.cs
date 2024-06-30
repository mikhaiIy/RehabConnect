using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class InvoiceBillingVM
    {
        public IEnumerable<Invoice> Invoice { get; set; }
        public IEnumerable<Billing> Billing { get; set; }

        public IEnumerable<InvoiceItem> Item { get; set; }
    }
}