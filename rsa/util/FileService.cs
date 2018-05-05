using RSA.errors;
using RSA.keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace RSA.util
{
    /// <summary>
    ///     Class that provides methods to export and import keys
    /// </summary>
    public class FileService
    {
        private const String PUB_KEY_FIRST_LINE = "BEGIN RSA PUBLIC KEY";
        private const String PUB_KEY_LAST_LINE = "END RSA PUBLIC KEY";
        private const String PRIV_KEY_FIRST_LINE = "BEGIN RSA PRIVATE KEY";
        private const String PRIV_KEY_LAST_LINE = "END RSA PRIVATE KEY";
       
        /// <summary>
        ///     Method to export public key in ASCII format
        /// </summary>
        /// <param name="publicKey">Public key. Instance of <see cref="PublicKey"/></param>
        /// <param name="path">Path, by witch the key will be saved</param>
        public void SavePublicKeyInFile(PublicKey publicKey, String path)
        {
            string base64Key = Convert.ToBase64String(Encoding.UTF8.GetBytes(publicKey.ToString()),
                Base64FormattingOptions.InsertLineBreaks);
            base64Key = PUB_KEY_FIRST_LINE + Environment.NewLine + Environment.NewLine + base64Key;
            base64Key += Environment.NewLine + Environment.NewLine + PUB_KEY_LAST_LINE;
            File.WriteAllText(path, base64Key);
        }

        /// <summary>
        ///     Method to export private key in ASCII format
        /// </summary>
        /// <param name="privateKey">Private key, instance of <see cref="PrivateKey"/></param>
        /// <param name="path">Path, by witch the key will be saved</param>
        public void SavePrivateKeyInFile(PrivateKey privateKey, String path)
        {
            string base64Key = Convert.ToBase64String(Encoding.UTF8.GetBytes(privateKey.ToString()),
                Base64FormattingOptions.InsertLineBreaks);
            base64Key = PRIV_KEY_FIRST_LINE + Environment.NewLine + Environment.NewLine + base64Key;
            base64Key += Environment.NewLine + Environment.NewLine + PRIV_KEY_LAST_LINE;
            File.WriteAllText(path, base64Key);
        }

        /// <summary>
        ///     Method to import public key from file
        /// </summary>
        /// <param name="path">Path where key locates</param>
        /// <returns>Public key, instance of <see cref="PublicKey"/></returns>
        public PublicKey GetPublicKeyFromFile(String path)
        {
            String[] lines = File.ReadAllLines(path);
            if (lines[0] != PUB_KEY_FIRST_LINE || lines[lines.Length - 1] != PUB_KEY_LAST_LINE || 
                lines[1] != "" || lines[lines.Length - 2] != "")
                    throw new IllegalKeyFileFormatException("Illegal key format!");
            String[] keyLines = new String[lines.Length - 4];
            Array.Copy(lines, 2, keyLines, 0, keyLines.Length);
            String base64Key = String.Join("", keyLines);
            String[] key = Encoding.UTF8.GetString(Convert.FromBase64String(base64Key)).Split(Environment.NewLine);
            if(key[0] != "modulus:" || key[2] != "public exponent:")
                throw new IllegalKeyFileFormatException("Illegal key format!");
            BigInteger modulus = BigInteger.Parse(key[1]);
            BigInteger publicExponent = BigInteger.Parse(key[3]);
            return new PublicKey(modulus, publicExponent);
        }

        /// <summary>
        ///     Method to import private key from file
        /// </summary>
        /// <param name="path">Path where private key locates</param>
        /// <returns>Private key, instance of <see cref="PrivateKey"/></returns>
        public PrivateKey GetPrivateKeyFromFile(String path)
        {
            String[] lines = File.ReadAllLines(path);
            if (lines[0] != PRIV_KEY_FIRST_LINE || lines[lines.Length - 1] != PRIV_KEY_LAST_LINE ||
                lines[1] != "" || lines[lines.Length - 2] != "")
                throw new IllegalKeyFileFormatException("Illegal key format!");
            String[] keyLines = new String[lines.Length - 4];
            Array.Copy(lines, 2, keyLines, 0, keyLines.Length);
            String base64Key = String.Join("", keyLines);
            String[] key = Encoding.UTF8.GetString(Convert.FromBase64String(base64Key)).Split(Environment.NewLine);
            if (key[0] != "modulus:" || key[2] != "private exponent:")
                throw new IllegalKeyFileFormatException("Illegal key format!");
            BigInteger modulus = BigInteger.Parse(key[1]);
            BigInteger privateExponent = BigInteger.Parse(key[3]);
            return new PrivateKey(modulus, privateExponent);
        }
    }
}
