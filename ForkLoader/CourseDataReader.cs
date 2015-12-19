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

        public List<Course> ReadCourses()
        {
            var courses = new List<Course>();
            using (var sr = new StreamReader(m_filename))
            {
                var ser = new XmlSerializer(typeof (CourseData));
                CourseData courseDatas = (CourseData)ser.Deserialize(sr);


            }
            return courses;
        }
    }
}
