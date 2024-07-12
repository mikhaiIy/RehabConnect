using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class ReportVM
    {
        [ValidateNever]
        public Report Report { get; set; }
        [ValidateNever]
        public Student Student { get; set; }
        [ValidateNever]
        public Program Program { get; set; }
        [ValidateNever]
        public Step Step { get; set; }
        [ValidateNever]
        public Roadmap Roadmap { get; set; }
        
    }
}
