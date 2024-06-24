using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.Models.ViewModel
{
    public class SelectChildVM
    {
        public Student Students { get; set; }
        public IEnumerable<Student> StudentList { get; set; }
    }
}
