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
        public string CourseName { get; set; }
        public string StartPointId { get; set; }
        public string FinishId { get; set; }

        public Course()
        {
            Controls = new List<int>();
        }
    }
}
