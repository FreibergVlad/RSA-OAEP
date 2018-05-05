using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rsa_test.command
{
    /// <summary>
    ///     Test class for <see cref="EncryptCommand"/>
    /// </summary>
    [TestClass]
    public class EncryptCommandTest
    {
        private String root_path = AppDomain.CurrentDomain.BaseDirectory;
        private String[] gen_keys = { "--gen-keys", "2048" };
        private String[] args1 = { "--encrypt", "id_rsa.pub", "test.txt", "test.enc"};
        private String[] args2 = { "--encrypt", "dfdsf", "fdfdf", "fdfdf"};
        private String[] args3 = { "--encrypt", "id_rsa_pub", "foo"};
        private String[] args4 = { "--encrypt", "iff" };

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
        public void WhenExecutedThenFileEncrypted()
        {
            new GenKeysCommand().Initialize(gen_keys).Execute();
            File.AppendAllText(args1[2], "Hello world!!!");
            new EncryptCommand().Initialize(args1).Execute();
            Assert.IsTrue(File.Exists(args1[3]));
            File.Delete(args1[1]);
            File.Delete(args1[2]);
            File.Delete(args1[3]);
        }

        [TestMethod]
        public void WhenExecutedWithWrongArguments()
        {
            new GenKeysCommand().Initialize(gen_keys).Execute();
            Assert.ThrowsException<ArgumentException>(() => new EncryptCommand().Initialize(args2).Execute());
            Assert.ThrowsException<ArgumentException>(() => new EncryptCommand().Initialize(args3).Execute());
            Assert.ThrowsException<ArgumentException>(() => new EncryptCommand().Initialize(args4).Execute());
        }
    }
}
