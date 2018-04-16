using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA.cipher.schemes;
using RSA.keygen;
using RSA.keys;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSATest.cipher.schemes
{
   [TestClass]
   public class RSAES_OAEP_Tests
    {
        private const int TEST_KEY_BIT_LENGTH = 2048;
        private const int TEST_KEY_OCTET_LENGTH = 256;
        private const int NUMBER_OF_TEST_ROUNDS = 4;
        private RSAES_OAEP oaep;
        private HashAlgorithm hash;
        private KeyGenerator keyGenerator;

        [TestInitialize]
        public void setUp()
        {
            oaep = new RSAES_OAEP();
            hash = new SHA512CryptoServiceProvider();
            keyGenerator = new KeyGenerator();
        }

        [TestMethod]
        public void RSA_OAEP_Encryption_Tests()
        {
            for(int i = 0; i < NUMBER_OF_TEST_ROUNDS; i++)
            {
                KeyPair keyPair = keyGenerator.GenerateKeys(TEST_KEY_BIT_LENGTH);
                RSA_OAEP_Encryption_Test("Hello world!!!", "", keyPair);
                RSA_OAEP_Encryption_Test("Cryptography is really awesome.", "", keyPair);
                RSA_OAEP_Encryption_Test("Проверка на совместимость с кириллицей", "", keyPair);
                RSA_OAEP_Encryption_Test("Специальные символы: ... !@#$%^&*()-", "", keyPair);
                // Testing with not-empty label
                RSA_OAEP_Encryption_Test("Hello world!!!", "213sadsdsad23", keyPair);
                RSA_OAEP_Encryption_Test("Cryptography is really awesome.", "132s^5dsadsd23", keyPair);
                RSA_OAEP_Encryption_Test("Проверка на совместимость с кириллицей", "alwewwelbasd", keyPair);
                RSA_OAEP_Encryption_Test("Специальные символы: ... !@#$%^&*()-", "zxcvbnm,<", keyPair);
            }
        }

        [TestMethod]
        public void EME_OAEP_Tests()
        {
            EME_OAEP_Test("Hello world!!!", "");
            EME_OAEP_Test("Cryptography is really awesome.", "");
            EME_OAEP_Test("Проверка на совместимость с кириллицей", "");
            EME_OAEP_Test("Специальные символы: ... !@#$%^&*()-", "");
        }

        private void RSA_OAEP_Encryption_Test(String message, String label, KeyPair keyPair)
        {
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            byte[] byteLabel = Encoding.UTF8.GetBytes(label);
            byte[] cipherText = oaep.RSAES_OAEP_Encrypt(keyPair.GetPublicKey, byteMessage, byteLabel, hash);
            byte[] decryptedText = oaep.RSAES_OAEP_Decrypt(keyPair.GetPrivateKey, cipherText, byteLabel, hash);
            CollectionAssert.AreEqual(byteMessage, decryptedText);
        }

        private void EME_OAEP_Test(String message, String label)
        {
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            byte[] byteLabel = Encoding.UTF8.GetBytes(label);
            byte[] encodedMessage = oaep.EME_OAEP_Encoding(byteMessage, byteLabel, TEST_KEY_OCTET_LENGTH, hash);
            byte[] decodedMessage = oaep.EME_OAEP_Decoding(encodedMessage, byteLabel, TEST_KEY_OCTET_LENGTH, hash);
            CollectionAssert.AreEqual(byteMessage, decodedMessage);
        }
    }
}
