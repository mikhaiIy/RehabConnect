using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class StudentProgVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int? RoadmapId { get; set; }
        public int? StepId { get; set; }
        public int? ProgramId { get; set; }
        public string? Status { get; set; }
        public IEnumerable<Roadmap>? Roadmap { get; set; }
        public IEnumerable<Step>? Step { get; set; }
        public IEnumerable<Program>? Program { get; set; }
        public IEnumerable<StudentProgram>? StudentProgram { get; set; }
    }
}