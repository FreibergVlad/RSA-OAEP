using RSA.cipher.schemes;
using RSA.errors;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSA.command
{
    /// <summary>
    ///     Implementation of <see cref="Command"/> interface. 
    ///     Class that represents --encrypt console command.
    ///     Command encrypts file with public key.
    /// </summary>
    public class EncryptCommand : Command
    {
        private FileService fileService;
        private RSAES_OAEP rsaes_oaep;
        private HashAlgorithm hash;
        private PublicKey publicKey;
        private byte[] plainText;
        private string pathToCipherText;
        private byte[] label = { };
        
        public void Execute()
        {
            Console.WriteLine("Encrypting...");
            byte[] c = rsaes_oaep.RSAES_OAEP_Encrypt(publicKey, plainText, label, hash);
            Console.WriteLine("Saving encrypted file to " + pathToCipherText);
            File.WriteAllBytes(pathToCipherText, c);
        }

        public Command Initialize(string[] args)
        {
            if (args.Length == 4)
            {
                try
                {
                    fileService = new FileService();
                    publicKey = fileService.GetPublicKeyFromFile(args[1]);
                    if (File.Exists(args[2]))
                        plainText = File.ReadAllBytes(args[2]);
                    else
                        throw new ArgumentException();
                    pathToCipherText = args[3];
                    rsaes_oaep = new RSAES_OAEP();
                    hash = new SHA512CryptoServiceProvider();
                    return this;

                } catch(Exception)
                {
                    throw new ArgumentException();
                } 
            }
            else
                throw new ArgumentException();
        }
    }
}
