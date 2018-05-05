using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rsa_test.command
{
    /// <summary>
    ///     Test class for <see cref="DecryptCommand"/>
    /// </summary>
    [TestClass]
    public class DecryptingCommandTest
    {

        private String root_path = AppDomain.CurrentDomain.BaseDirectory;
        private String[] gen_keys = { "--gen-keys", "2048" };
        private String[] encrypt = { "--encrypt", "id_rsa.pub", "test.txt", "test.cipher"};
        private String[] args1 = { "--decrypt", "id_rsa", "test.cipher", "test.dec" };
        private String[] args2 = { "--decrypt", "dfdsf", "fdfdf", "fdfdf" };
        private String[] args3 = { "--decrypt", "id_rsa_pub", "foo" };
        private String[] args4 = { "--decrypt", "iff" };

        [TestInitialize]
        public void SetUp()
        {
            args1[1] = root_path + args1[1];
            args1[2] = root_path + args1[2];
            args1[3] = root_path + args1[3];
            args2[1] = root_path + args2[1];
            args2[2] = root_path + args2[2];
            args2[3] = root_path + args2[3];
            args3[1] = root_path + args3[1];
            args3[2] = root_path + args3[2];
            args4[1] = root_path + args4[1];
        }

        [TestMethod]
        public void WhenExecutedThenFileDecrypted()
        {
            GenerateKeys();
            EncryptFile();
            new DecryptCommand().Initialize(args1).Execute();
            CollectionAssert.AreEqual(File.ReadAllBytes(encrypt[2]), File.ReadAllBytes(args1[3]));
        }

        [TestMethod]
        public void WhenExecutedWithWrongArguments()
        {
            Assert.ThrowsException<ArgumentException>(() => new EncryptCommand().Initialize(args2));
            Assert.ThrowsException<ArgumentException>(() => new EncryptCommand().Initialize(args3));
            Assert.ThrowsException<ArgumentException>(() => new EncryptCommand().Initialize(args4));
        }

        private void EncryptFile()
        {
            File.AppendAllText(encrypt[2], "!!! TEST DATA !!!");
            new EncryptCommand().Initialize(encrypt).Execute();
        }

        private void GenerateKeys()
        {
            new GenKeysCommand().Initialize(gen_keys).Execute();
        }
    }
}
