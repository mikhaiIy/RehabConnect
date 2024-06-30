using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class InvoiceAddVM
    {
        public Invoice? Invoice { get; set; }

        public InvoiceItem?  Item { get; set; }

    }
}