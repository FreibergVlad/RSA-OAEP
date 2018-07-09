using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSA.util
{
    /// <summary>
    ///     Service class that provides methods to work with symmetric cryptography
    /// </summary>
    public class SymmetricCryptoService
    {

        private static readonly byte[] SALT = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };

        public byte[] Encrypt(byte[] data, string passphrase)
        {
            // Check arguments.
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (passphrase == null || passphrase.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            byte[] IV;
            string plainText = Encoding.UTF8.GetString(data);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = CreateKey(passphrase);
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                IV = aesAlg.IV;
                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Write IV to the encrypted data header
            byte[] result = ByteArraysUtils.Concat(IV, encrypted);
            // Return the encrypted bytes from the memory stream.
            return result;

        }

        public byte[] Decrypt(byte[] cipherText, string passphrase)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (passphrase == null || passphrase.Length <= 0)
                throw new ArgumentNullException("Key");
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            byte[] IV = new byte[16];
            byte[] encrypted = new byte[cipherText.Length - 16];
            System.Buffer.BlockCopy(cipherText, 0, IV, 0, IV.Length);
            System.Buffer.BlockCopy(cipherText, IV.Length, encrypted, 0, encrypted.Length);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = CreateKey(passphrase);
                aesAlg.IV = IV;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return Encoding.UTF8.GetBytes(plaintext);

        }

        private byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), SALT, Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}
