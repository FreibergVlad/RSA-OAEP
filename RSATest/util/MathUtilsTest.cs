using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSATest.util
{
    [TestClass]
    public class MathUtilsTest
    {
        private MathUtils mathUtils;

        [TestInitialize]
        public void SetUp()
        {
            mathUtils = new MathUtils();
        }

        [TestMethod]
        public void ModInverse()
        {
            Assert.AreEqual(4, mathUtils.ModInverse(3, 11));
            Assert.AreEqual(12, mathUtils.ModInverse(10, 17));
            Assert.AreEqual(9, mathUtils.ModInverse(3, 26));
            Assert.AreEqual(37, mathUtils.ModInverse(13, 120));
        }
    }
}
