using System;

namespace RehabConnect.ViewModels
{
    public class SessionViewModel
    {
        public int SessionID { get; set; }
        public string StudentName { get; set; }
        public int TherapistID{ get; set; }
        public string ProgramName { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; }
    }
}

