using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<ParentDetail> ParentDetails { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Therapist> Therapists { get; set; }
        public IEnumerable<CustomerSupport> CustomerSupports { get; set; }
    }
}
