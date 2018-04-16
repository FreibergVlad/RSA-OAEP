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
    public class FileService
    {
        private const String PUBLIC_KEY_FILE_FIRST_LINE = "BEGIN RSA PUBLIC KEY";
        private const String PUBLIC_KEY_FILE_LAST_LINE = "END RSA PUBLIC KEY";
        private const String PRIVATE_KEY_FILE_FIRST_LINE = "BEGIN RSA PRIVATE KEY";
        private const String PRIVATE_KEY_FILE_LAST_LINE = "END RSA PRIVATE KEY";

        public void SavePublicKeyInFile(PublicKey key, String path)
        {
            BigInteger modulus = key.GetModulus;
            BigInteger e = key.GetPublicExponent;
            String keyStr = "Modulus:\r\n" + modulus + "\r\nExponent:\r\n" + e;
            String base64Key = PUBLIC_KEY_FILE_FIRST_LINE+"\r\n"+Convert.ToBase64String(Encoding.UTF8.GetBytes(keyStr))+"\r\n"+PUBLIC_KEY_FILE_LAST_LINE;
            File.WriteAllText(path, base64Key);
        }

        public PublicKey GetPublicKeyFromFile(String path)
        {
            String[] fileLines = File.ReadAllLines(path);
            if (!fileLines[0].Equals(PUBLIC_KEY_FILE_FIRST_LINE) || !fileLines[2].Equals(PUBLIC_KEY_FILE_LAST_LINE))
                throw new IllegalKeyFileFormatException("Illegal public key file format");
            String[] keyStr = (Encoding.UTF8.GetString(Convert.FromBase64String(fileLines[1]))).Split("\r\n");
            if(!keyStr[0].Equals("Modulus:") || !keyStr[2].Equals("Exponent:"))
                throw new IllegalKeyFileFormatException("Illegal public key file format");
            BigInteger modulus = BigInteger.Parse(keyStr[1]);
            BigInteger e = BigInteger.Parse(keyStr[3]);
            return new PublicKey(modulus, e);
        }

        public PublicKey GetPrivateKeyFromFile(String password, String path)
        {
            return null;
        }

        public void SavePrivateKeyInFile(PrivateKey privateKey, String password, String path)
        {

        }
    }
}
