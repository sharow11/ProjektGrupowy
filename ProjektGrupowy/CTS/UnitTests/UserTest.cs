﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CTS.UnitTests
{
    [TestFixture]
    public class UserTest
    {
        [TestCase("Ahmed")]

        public void Test(string name)
        {
            var user = new User();
            user.Name = name;
            Assert.That(String.Equals(user.Name, name));
        }
    }
}
