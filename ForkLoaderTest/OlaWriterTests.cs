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
    public class OlaWriterTests
    {
        [Test]
        public void CanCreateOlaWriter()
        {
            var sut = new OlaWriter(string.Empty, 1);
        }
    }
}
