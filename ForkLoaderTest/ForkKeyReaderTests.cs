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
    public class ForkKeyReaderTests
    {
        [Test]
        public void Can_read_forks()
        {
            var sut = new ForkKeyReader(@"..\..\..\TestData\ForkTest1.csv");
            List<ForkKey> keys = sut.ReadForkKeys();
            Assert.AreEqual(6, keys.Count);
            Assert.IsTrue(keys.All(k => k.Forks.Count == 3));
            Assert.AreEqual(6, keys[5].TeamNumber);
            Assert.IsTrue(string.Equals(keys[3].Forks[1], "A3"));
        }
    }
}
