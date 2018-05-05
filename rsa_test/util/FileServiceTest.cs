using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.keygen;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RSATest.util
{
    /// <summary>
    ///     Tests for <see cref="FileService"/>
    /// </summary>
    [TestClass]
    public class FileServiceTest
    {
        private FileService fileService;
        private KeyGenerator keyGenerator;
        private KeyPair keyPair;
        private const string PATH_TO_PUB_KEY = "test_pub.asc";
        private const string PATH_TO_PRIV_KEY = "test_priv.asc";

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
            fileService.SavePublicKeyInFile(publicKey, PATH_TO_PUB_KEY);
            PublicKey actual = fileService.GetPublicKeyFromFile(PATH_TO_PUB_KEY);
            Assert.AreEqual(publicKey, actual);
            File.Delete(PATH_TO_PUB_KEY);
        }

        [TestMethod]
        public void SavePrivateKeyInFile()
        {
            PrivateKey privateKey = keyPair.GetPrivateKey;
            fileService.SavePrivateKeyInFile(privateKey, PATH_TO_PRIV_KEY);
            PrivateKey actual = fileService.GetPrivateKeyFromFile(PATH_TO_PRIV_KEY);
            Assert.AreEqual(privateKey, actual);
            File.Delete(PATH_TO_PRIV_KEY);
        }
    }
}
