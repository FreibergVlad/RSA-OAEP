using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSATest.util
{
    /// <summary>
    ///     Tests for <see cref="ByteArraysUtils"/>
    /// </summary>
    [TestClass]
    public class ByteArraysUtilsTest
    {

        [TestMethod]
        public void Concat()
        {
            byte[] arg1 = { 97, 29, 31, 55, 17, 0, 129 };
            byte[] arg2 = { 12, 33, 14, 40, 199, 244, 87 };
            byte[] expected = { 97, 29, 31, 55, 17, 0, 129, 12, 33, 14, 40, 199, 244, 87 };
            CollectionAssert.AreEqual(expected, ByteArraysUtils.Concat(arg1, arg2));
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
            byte[] arg1 = { 97, 29, 31, 55, 17, 0, 129 };
            byte[] expected1 = { 31, 55, 17, 0 };
            byte[] expected2 = { 97, 29};
            CollectionAssert.AreEqual(expected1, ByteArraysUtils.GetSubArray(arg1, 2, 4));
            CollectionAssert.AreEqual(expected2, ByteArraysUtils.GetSubArray(arg1, 0, 2));
        }
    }
}
