using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkLoader;
using NUnit.Framework;

namespace ForkLoaderTest
{
    [TestFixture]
    public class ForkKeysValidatorTests
    {

        [Test]
        public void CanValidateCorrectForks()
        {
            var sut = new ForkKeysValidator(CreateCorrectForkKeys(), CreateCourses());
            Assert.IsTrue(sut.Validate());
        }

        [Test]
        public void CanValidateCorrectForks2()
        {
            var sut = new ForkKeysValidator(CreateCorrectForkKeys2(), CreateCourses());
            Assert.IsTrue(sut.Validate());
        }

        [Test]
        public void CanDetectForkError()
        {
            var sut = new ForkKeysValidator(CreateFaultyForkKeys(), CreateCourses());
            Assert.IsFalse(sut.Validate());
        }

        [Test]
        public void CanDetectForkError2()
        {
            var sut = new ForkKeysValidator(CreateFaultyForkKeys2(), CreateCourses());
            Assert.IsFalse(sut.Validate());
        }

        private Dictionary<string, ForkLoader.Course> CreateCourses()
        {
            var result = new Dictionary<string, ForkLoader.Course>();
            result.Add("A", new ForkLoader.Course()
            {
                CourseName =  "A",
                StartPointId = "S",
                FinishId = "M",
                Controls = new List<int> { 31, 32, 33, 34, 35}
            });

            result.Add("B", new ForkLoader.Course()
            {
                CourseName = "B",
                StartPointId = "S",
                FinishId = "M",
                Controls = new List<int> { 36, 32, 33, 34, 35 }
            });

            result.Add("C", new ForkLoader.Course()
            {
                CourseName = "C",
                StartPointId = "S",
                FinishId = "M",
                Controls = new List<int> { 31, 32, 33, 37, 35 }
            });

            result.Add("D", new ForkLoader.Course()
            {
                CourseName = "D",
                StartPointId = "S",
                FinishId = "M",
                Controls = new List<int> { 36, 32, 33, 37, 35 }
            });
            

            
            return result;
        }


        private List<ForkKey> CreateCorrectForkKeys()
        {
            var result = new List<ForkKey>();
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "B", "C"}
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "B", "A", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "B", "C" }
            });
            return result;
        }

        private List<ForkKey> CreateCorrectForkKeys2()
        {
            var result = new List<ForkKey>();
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "B", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "B", "A", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "D", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "A", "D" }
            });
            return result;
        }

        private List<ForkKey> CreateFaultyForkKeys()
        {
            var result = new List<ForkKey>();
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "B", "B", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "B", "A", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "B", "C" }
            });
            return result;
        }

        private List<ForkKey> CreateFaultyForkKeys2()
        {
            var result = new List<ForkKey>();
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "B", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "B", "A", "C" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "C", "B", "A" }
            });
            result.Add(new ForkKey
            {
                ClassId = 1,
                TeamNumber = 1,
                Forks = new List<string> { "A", "D", "C" }
            });
            return result;
        }
    }
}
