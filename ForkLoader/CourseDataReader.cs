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

        public Dictionary<string, Course> ReadCourses()
        {
            var courses = new Dictionary<string, Course>();
            using (var sr = new StreamReader(m_filename))
            {
                var ser = new XmlSerializer(typeof (CourseData));
                CourseData courseDatas = (CourseData)ser.Deserialize(sr);
                foreach (RaceCourseData raceCourseData in courseDatas.RaceCourseData)
                {
                    foreach (global::Course iofCourse in raceCourseData.Course)
                    {
                        var course = new Course();
                        course.CourseId = Convert.ToInt32(iofCourse.Id);
                        course.CourseName = iofCourse.Name;
                        foreach (CourseControl item in iofCourse.CourseControl)
                        {
                            if (item.type == ControlType.Start)
                            {
                                course.StartPointId = item.Control.Single();
                            }
                            else if (item.type == ControlType.Control)
                            {
                                course.Controls.Add(Convert.ToInt32(item.Control.Single()));
                            }
                            else if (item.type == ControlType.Finish)
                            {
                                course.FinishId = item.Control.Single();
                            }
                            else
                            {
                                m_loggger.WarnFormat("Unexpected control type detected {0}", item.type);
                            }
                        }
                        courses.Add(course.CourseName, course);
                    }
                }
            }
            return courses;
        }
    }
}
