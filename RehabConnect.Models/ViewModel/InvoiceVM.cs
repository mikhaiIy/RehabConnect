using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class InvoiceVM
    {
        //program, step 
        [ValidateNever]
        public IEnumerable<Step>? Steps { get; set; }
        public IEnumerable<Program>? ProgramList { get; set; }

        public string TypeOfDay { get; set; }

    }
}