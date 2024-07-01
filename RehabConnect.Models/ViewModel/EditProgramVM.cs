using Microsoft.AspNetCore.Mvc.Rendering;
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
    }
}
