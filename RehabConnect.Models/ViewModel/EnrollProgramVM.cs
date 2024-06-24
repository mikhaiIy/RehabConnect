using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class EnrollProgramVM
    {
        public IEnumerable<Schedule> Schedule { get; set; }

        [ValidateNever]
        public StudentProgram? StudentProgram { get; set; }

        public Step? Steps { get; set; }

        public IEnumerable<Program> ProgramList { get; set; }

        public string ScheduleDataJson { get; set; }
    }
}
