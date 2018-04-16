using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.cipher;
using RSA.keygen;
using RSA.keys;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSATest.cipher
{
    [TestClass]
    public class CryptoPrimitivesTest
    {
        private const int NUMBER_OF_TEST_ROUNDS = 4;
        private DataPrimitives dataPrimitives;
        private CryptoPrimitives cryptoPrimitives;
        private KeyGenerator keyGenerator;

        [TestInitialize]
        public void SetUp()
        {
            dataPrimitives = new DataPrimitives();
            cryptoPrimitives = new CryptoPrimitives();
            keyGenerator = new KeyGenerator();
        }

        [TestMethod]
        public void TestCryptoPrimitives()
        {
            for(int i = 0; i<NUMBER_OF_TEST_ROUNDS; i++)
            {
                KeyPair testKeyPair = keyGenerator.GenerateKeys(2048);
                TestCryptoPrimitive("Hello world", testKeyPair);
                TestCryptoPrimitive("Проверка совместимости с кириллицей!!!", testKeyPair);
                TestCryptoPrimitive("Cryptography is awesome!", testKeyPair);
                TestCryptoPrimitive("Специальные символы: !№;%:?*()_+=- \\..,,//:;", testKeyPair);
                TestCryptoPrimitive("", testKeyPair);
            }
        }

        private void TestCryptoPrimitive(String testString, KeyPair testKeyPair)
        {
            BigInteger integer = dataPrimitives.OS2IP(Encoding.UTF8.GetBytes(testString));
            BigInteger cipherText = cryptoPrimitives.RSAEP(testKeyPair.GetPublicKey, integer);
            BigInteger decryptedText = cryptoPrimitives.RSADP(testKeyPair.GetPrivateKey, cipherText);
            Assert.AreEqual(integer, decryptedText);
        }

    }
}
