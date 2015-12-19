using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class Course
    {
        private List<int> m_controls;
        private string m_courseId;
        private string m_startPointId;
        private string m_finishId;

        public Course(string courseId)
        {
            m_courseId = courseId;
        }
    }
}
