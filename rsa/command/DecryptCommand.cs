using RSA.cipher.schemes;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSA.command
{
    public class DecryptCommand : Command
    {
        /// <summary>
        ///     Implementation of <see cref="Command"/> interface. 
        ///     Class that represents --decrypt console command.
        ///     Command decrypts file with private key.
        /// </summary>
        private FileService fileService;
        private RSAES_OAEP rsaes_oaep;
        private HashAlgorithm hash;
        private PrivateKey privateKey;
        private byte[] cipherData;
        private String pathToDecryptedData;
        private byte[] label = { };

        public void Execute()
        {
            Console.WriteLine("Decrypting...");
            byte[] m = rsaes_oaep.RSAES_OAEP_Decrypt(privateKey, cipherData, label, hash);
            Console.WriteLine("Saving decrypted file to " + pathToDecryptedData);
            File.WriteAllBytes(pathToDecryptedData, m);
        }

        public Command Initialize(string[] args)
        {
            if(args.Length == 4)
            {
                fileService = new FileService();
                privateKey = fileService.GetPrivateKeyFromFile(args[1]);
                if (File.Exists(args[2]))
                    cipherData = File.ReadAllBytes(args[2]);
                else
                    throw new ArgumentException();
                pathToDecryptedData = args[3];
                rsaes_oaep = new RSAES_OAEP();
                hash = new SHA512CryptoServiceProvider();
                return this;
            } else
            {
                throw new ArgumentException();
            }
        }
    }
}
