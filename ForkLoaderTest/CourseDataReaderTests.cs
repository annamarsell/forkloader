using ForkLoader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoaderTest
{
    [TestFixture]
    public class CourseDataReaderTests
    {
        [Test]
        public void CanReadCourseData()
        {
            var sut = new CourseDataReader(@"..\..\..\TestData\T140828_coursedata.xml");
           // var sut = new CourseDataReader(@"..\..\..\TestData\CourseData_example1.xml");
            List<ForkLoader.Course> courses = sut.ReadCourses();
        }
    }
}
