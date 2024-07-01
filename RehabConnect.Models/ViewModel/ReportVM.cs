﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public Report Report { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> StudentList { get; set; }
        
        public IEnumerable<SelectListItem> ReportList { get; set; }
        
    }
}
