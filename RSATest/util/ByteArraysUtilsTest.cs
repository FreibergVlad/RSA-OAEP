using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSATest.util
{
    [TestClass]
    public class ByteArraysUtilsTest
    {

        private ByteArraysUtils byteArraysUtils;

        [TestInitialize]
        public void SetUp()
        {
            byteArraysUtils = new ByteArraysUtils();
        }

        [TestMethod]
        public void Concat()
        {

        }

        [TestMethod]
        public void XorBytes()
        {
            byte[] arg1 = {97, 29, 31, 55, 17, 0, 129};
            byte[] arg2 = {12, 33, 14, 40, 199, 244, 87};
            byte[] xor = ByteArraysUtils.XorBytes(arg1, arg2);
            byte[] actual = ByteArraysUtils.XorBytes(xor, arg2);
            CollectionAssert.AreEqual(arg1, actual);
        }

        [TestMethod]
        public void GetSubArray()
        {

        }
    }
}
