using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rsa_test.command
{
    /// <summary>
    ///     Test class for <see cref="GenKeysCommand"/>
    /// </summary>
    [TestClass]
    public class GenKeysCommandTest
    {

        private String[] args1 = { "--gen-keys", "2048" };
        private String[] args2 = { "--gen-keys", "512" };
        private String[] args3 = { "--gen-keys"};
        private String[] args4 = { "--gen-keys", "iff" };

        [TestMethod]
        public void WhenExecutedThenKeysCreatedAndSavedInFile()
        {
            new GenKeysCommand().Initialize(args1).Execute();
            Assert.IsTrue(File.Exists("id_rsa"));
            Assert.IsTrue(File.Exists("id_rsa.pub"));
            File.Delete("id_rsa");
            File.Delete("id_rsa.pub");
        }

        [TestMethod]
        public void WhenExecutedWithWrongArguments()
        {
            Assert.ThrowsException<ArgumentException>(() => new GenKeysCommand().Initialize(args2).Execute());
            Assert.ThrowsException<ArgumentException>(() => new GenKeysCommand().Initialize(args3).Execute());
            Assert.ThrowsException<ArgumentException>(() => new GenKeysCommand().Initialize(args4).Execute());
        }
    }
}
