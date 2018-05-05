using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.cipher;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSATest.cipher
{
    /// <summary>
    ///     Tests for <see cref="DataPrimitives"/>
    /// </summary>
    [TestClass]
    public class DataPrimitivesTest
    {
        private DataPrimitives dataPrimitives;

        [TestInitialize]
        public void SetUp()
        {
            dataPrimitives = new DataPrimitives();
        }

        [TestMethod]
        public void TestDataPrimitives()
        {
            TestDataPrimitive("Hello world!");
            TestDataPrimitive("test test Test");
            TestDataPrimitive("Cryptography is awesome! ITS REALLY AWESOME AND USEFUL");
            TestDataPrimitive("Проверка на совместимость с кириллицей!!!");
            TestDataPrimitive("");
            TestDataPrimitive("Буквы и 4342343243243243442");
            TestDataPrimitive("И специальные знаки ,---+=\\>><<<!@#$*&?/");
        }

        private void TestDataPrimitive(string testString)
        {
            BigInteger integer = dataPrimitives.OS2IP(Encoding.UTF8.GetBytes(testString));
            byte[] bytes = dataPrimitives.I2OSP(integer, Encoding.UTF8.GetBytes(testString).Length);
            string actual = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            Assert.AreEqual(testString, actual);
        }
    }
}
