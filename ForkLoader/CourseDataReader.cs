using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using log4net;

namespace ForkLoader
{
    public class CourseDataReader
    {
        private readonly string m_filename;
        private static readonly ILog m_loggger = LogManager.GetLogger(typeof(ForkKeyReader));

        public CourseDataReader(string filename)
        {
            m_filename = filename;
        }

        public Dictionary<int, Course> ReadCourses()
        {
            var courses = new Dictionary<int, Course>();
            using (var sr = new StreamReader(m_filename))
            {
                var ser = new XmlSerializer(typeof (CourseData));
                CourseData courseDatas = (CourseData)ser.Deserialize(sr);
                foreach (global::Course iofCourse in courseDatas.Course)
                {
                    var course = new Course();
                    course.CourseId = Convert.ToInt32(iofCourse.CourseId);
                    foreach (object item in iofCourse.Items)
                    {
                        if (item is StartPoint)
                        {
                            course.StartPointId = Convert.ToInt32((item as StartPoint).StartPointCode);
                        }
                        else if (item is Control)
                        {
                            course.Controls.Add(Convert.ToInt32((item as Control).ControlCode));
                        }
                        else if(item is FinishPoint)
                        {
                            course.FinishId = Convert.ToInt32((item as FinishPoint).FinishPointCode);
                        }
                    }
                    courses.Add(course.CourseId, course);
                }
            }
            return courses;
        }
    }
}
