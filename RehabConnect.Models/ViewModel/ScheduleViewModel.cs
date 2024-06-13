using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class ScheduleViewModel
    {
        public string Day { get; set; }
        public List<PeriodViewModel> Periods { get; set; }
    }

    public class PeriodViewModel
    {
        public TimeSpan PeriodTime { get; set; }
        public string Subject { get; set; }
    }
}
