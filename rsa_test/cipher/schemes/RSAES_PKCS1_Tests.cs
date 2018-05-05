using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.cipher.schemes;
using RSA.keygen;
using RSA.keys;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSATest.cipher.schemes
{
    /// <summary>
    ///     Tests for <see cref="RSAES_PKCS1"/>
    /// </summary>
    [TestClass]
    public class RSAES_PKCS1_Tests
    {
        private const int TEST_KEY_BIT_LENGTH = 2048;
        private const int TEST_KEY_OCTET_LENGTH = 256;
        private const int NUMBER_OF_TEST_ROUNDS = 4;
        private RSAES_PKCS1 rsaes_pkcs1;
        private KeyGenerator keyGenerator;

        [TestInitialize]
        public void SetUp()
        {
            rsaes_pkcs1 = new RSAES_PKCS1();
            keyGenerator = new KeyGenerator();
        }

        [TestMethod]
        public void EME_PKCS_EncodingAndDecodingTest()
        {
            EME_PKCS1_Test("Hello world!!!");
            EME_PKCS1_Test("Cryptography is awesome!!1111");
            EME_PKCS1_Test("Проверка совместимости с кириллицей");
            EME_PKCS1_Test("Специальные символы: !\"№;%:?*()_+=\\.// ");
        }

        [TestMethod]
        public void RSAES_PKCS1_EncryptionAndDecryptionTest()
        {
            for(int i = 0; i< NUMBER_OF_TEST_ROUNDS; i++)
            {
                KeyPair testKeyPair = keyGenerator.GenerateKeys(2048);
                Encryption_PKCS1_Test("Hello world!", testKeyPair);
                Encryption_PKCS1_Test("Cryptography is really awesome and useful", testKeyPair);
                Encryption_PKCS1_Test("Проверка на совместимость с кириллицей", testKeyPair);
                Encryption_PKCS1_Test("Some other symbols... \"!@#$%^&*\\--", testKeyPair);
                Encryption_PKCS1_Test("", testKeyPair);
            }
        }

        private void EME_PKCS1_Test(String plainText)
        {
            byte[] message = Encoding.UTF8.GetBytes(plainText);
            byte[] EM = rsaes_pkcs1.EME_PKCS1_Encoding(TEST_KEY_OCTET_LENGTH, message);
            byte[] M = rsaes_pkcs1.EME_PKCS1_Decoding(EM);
            CollectionAssert.AreEqual(message, M);
        }

        private void Encryption_PKCS1_Test(String plainText, KeyPair testKeyPair)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherText = rsaes_pkcs1.RSAES_PKCS1_Encrypt(testKeyPair.GetPublicKey, bytes);
            byte[] decryptText = rsaes_pkcs1.RSAES_PKCS1_Decrypt(testKeyPair.GetPrivateKey, cipherText);
            CollectionAssert.AreEqual(bytes, decryptText);
        }
    }
}
