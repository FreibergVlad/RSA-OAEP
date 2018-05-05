using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSATest.util
{
    /// <summary>
    ///     Tests for <see cref="MathUtils"/>
    /// </summary>
    [TestClass]
    public class MathUtilsTest
    {
        
        [TestMethod]
        public void ModInverse()
        {
            Assert.AreEqual(4, MathUtils.ModInverse(3, 11));
            Assert.AreEqual(12, MathUtils.ModInverse(10, 17));
            Assert.AreEqual(9, MathUtils.ModInverse(3, 26));
            Assert.AreEqual(37, MathUtils.ModInverse(13, 120));
        }
    }
}
