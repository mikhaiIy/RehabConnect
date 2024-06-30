using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace RehabConnect.Models.ViewModel
{
    public class ScheduleViewModel
    {
        public List<PeriodViewModel> Periods { get; set; }

        public IEnumerable<Roadmap> RoadmapList { get; set; }
        public IEnumerable<Step> StepList { get; set; }
        public IEnumerable<Program>? ProgramList { get; set; }

       
    }

    public class PeriodViewModel
    {
        public TimeSpan PeriodTime { get; set; }
        public string Subject { get; set; }
    }
}
