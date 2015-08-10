using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace DAL.UnitTests
{
    [TestFixture]
    class UtilitiesTest
    {
        [TestCase("Create Database")]
        public void Test(string name)
        {
            string newFile = DatabaseFileUtilities.Filename;
            Assert.That(null != newFile);

            DatabaseFileUtilities.CreateFile();
            Assert.That(File.Exists(newFile));

            var fi = new FileInfo(newFile);
            Assert.That(fi.Length > 0);
        }
    }
}
