using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TeamWin.Propinquity.Web.Tests
{
    [TestFixture]
    public class NameFactoryTests
    {
        [Test]
        public void Test()
        {
            var name1 = NameFactory.GetName();
            var name2 = NameFactory.GetName();
            Assert.That(name1, Is.Not.EqualTo(name2));
        }
    }
}
