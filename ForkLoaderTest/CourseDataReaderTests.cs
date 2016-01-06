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
           // var sut = new CourseDataReader(@"..\..\..\TestData\T140828_coursedata.xml");
            var sut = new CourseDataReader(@"..\..\..\TestData\CourseTestData.xml");
            Dictionary<string, ForkLoader.Course> courses = sut.ReadCourses();
            Assert.IsTrue(courses.ContainsKey("Orange"));
            ForkLoader.Course orangeCourse = courses["Orange"];
            Assert.AreEqual(5, orangeCourse.Controls.Count);
            Assert.AreEqual(52, orangeCourse.Controls[3]);
        }
    }
}
