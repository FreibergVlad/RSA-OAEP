using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.keygen;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSATest.util
{
    [TestClass]
    public class FileServiceTest
    {
        private FileService fileService;
        private KeyGenerator keyGenerator;
        private KeyPair keyPair;

        [TestInitialize]
        public void SetUp()
        {
            fileService = new FileService();
            keyGenerator = new KeyGenerator();
            keyPair = keyGenerator.GenerateKeys(4096);
        }

        [TestMethod]
        public void SavePublicKeyInFile()
        {
            PublicKey publicKey = keyPair.GetPublicKey;
            fileService.SavePublicKeyInFile(publicKey, "test_public_key.txt");
            PublicKey actual = fileService.GetPublicKeyFromFile("test_public_key.txt");
            Assert.AreEqual(publicKey, actual);
        }

        [TestMethod]
        public void SavePrivateKeyInFile()
        {

        }
    }
}
