﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class EditProgramVM
    {
        public IEnumerable<SelectListItem> ProgramSelectList { get; set; }
        public StudentProgram studentProgram {  get; set; }

        public int progid { get; set; }

        public int stuId { get; set; }
        
        public int studProg { get; set; }
        public StudentStatus Status { get; set; }
        
        public IEnumerable<SelectListItem> StatusList { get; set; }
        
    }
}
