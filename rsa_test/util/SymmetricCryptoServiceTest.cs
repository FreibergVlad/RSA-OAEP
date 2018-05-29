using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace rsa_test.util
{
    [TestClass]
    public class SymmetricCryptoServiceTest
    {
        private SymmetricCryptoService symmetricCryptoService;

        [TestInitialize]
        public void SetUp()
        {
            symmetricCryptoService = new SymmetricCryptoService();
        }

        [TestMethod]
        public void WhenExecutedWithCorrectPassphrase()
        {
            byte[] plain = Encoding.UTF8.GetBytes("Hello world!!!sfasfsdfsafasffsfasdfdfsafsfsdfasfsfsdfsafsadfsf4324234фывыфвыфвфывывыфвывывфвывфывыфвфы");
            string passphrase = "password";
            byte[] cipher = symmetricCryptoService.Encrypt(plain, passphrase);
            byte[] decrypt = symmetricCryptoService.Decrypt(cipher, passphrase);
            CollectionAssert.AreEqual(plain, decrypt);
        }

        [TestMethod]
        public void WhenExecutedWithWrongPassphrase()
        {
            byte[] plain = Encoding.UTF8.GetBytes("Hello world!!!");
            string passphrase = "password";
            byte[] cipher = symmetricCryptoService.Encrypt(plain, passphrase);
            Assert.ThrowsException<CryptographicException>(() => symmetricCryptoService.Decrypt(cipher, "Wrong sdfdsadfasfasfsfsfapassphrase"));
        }
    }
}
