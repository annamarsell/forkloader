using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class Course
    {
        public List<int> Controls { get; set; }
        public int CourseId { get; set; }
        public int StartPointId { get; set; }
        public int FinishId { get; set; }

        public Course()
        {
            Controls = new List<int>();
        }
    }
}
