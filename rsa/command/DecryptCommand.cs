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
    public class DecryptCommand : Command
    {
        /// <summary>
        ///     Implementation of <see cref="Command"/> interface. 
        ///     Class that represents --decrypt console command.
        ///     Command decrypts file with private key.
        /// </summary>
        private FileService fileService;
        private ConsoleUtils consoleUtils;
        private RSAES_OAEP rsaes_oaep;
        private HashAlgorithm hash;
        private PrivateKey privateKey;
        // Data need to be decrypted
        private byte[] cipherData;
        private String pathToDecryptedData;
        // Optional label (see PKCS #1 v2.2 for more details)
        private byte[] label = { };
        private bool initialized = false;

        public void Execute()
        {
            if (!isInitialized())
                throw new CommandNotInitializedException();
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
                consoleUtils = new ConsoleUtils();
                Console.WriteLine("You need a passphrase to unlock a private key.");
                Console.Write("Enter a passphrase: ");
                privateKey = fileService.GetPrivateKeyFromFile(args[1], consoleUtils.GetPassword());
                if (File.Exists(args[2]))
                    cipherData = File.ReadAllBytes(args[2]);
                else
                    throw new ArgumentException();
                pathToDecryptedData = args[3];
                rsaes_oaep = new RSAES_OAEP();
                hash = new SHA512CryptoServiceProvider();
                initialized = true;
                return this;
            } else
                throw new ArgumentException();
            
        }

        public bool isInitialized() => initialized;
    }
}
