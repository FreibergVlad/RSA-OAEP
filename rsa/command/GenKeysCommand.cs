using RSA.errors;
using RSA.keygen;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.command
{
    /// <summary>
    ///     Implementation of <see cref="Command"/> interface. 
    ///     Class that represents --gen-keys console command.
    ///     Commands generates RSA key pair of required length and save it in file.
    /// </summary>
    public class GenKeysCommand : Command
    {

        private const int MIN_KEY_SIZE = 2048;
        private const string PUB_KEY_PATH = "id_rsa.pub";
        private const string PRIV_KEY_PATH = "id_rsa";

        private KeyGenerator keyGenerator;
        private FileService fileService;
        private ConsoleUtils consoleUtils;
        private int keySize;
        private bool initialized = false;

        public void Execute()
        {
            if (!isInitialized())
                throw new CommandNotInitializedException();
            Console.WriteLine("Generating RSA " + keySize + " bits key pair...");
            KeyPair keyPair = keyGenerator.GenerateKeys(keySize);
            Console.WriteLine("Saving key pair in... " + AppDomain.CurrentDomain.BaseDirectory);
            fileService.SavePublicKeyInFile(keyPair.GetPublicKey, PUB_KEY_PATH);
            Console.WriteLine("You need a passphrase to protect your private key.");
            Console.Write("Enter a passphrase: ");
            fileService.SavePrivateKeyInFile(keyPair.GetPrivateKey, PRIV_KEY_PATH, consoleUtils.GetPassword());
            Console.WriteLine();
            Console.WriteLine("Key pair were saved in " + AppDomain.CurrentDomain.BaseDirectory);
        }

        public Command Initialize(String[] args)
        {
            if (args.Length == 2)
            {
                try
                {
                    keySize = Convert.ToInt32(args[1]);
                    if (keySize < MIN_KEY_SIZE)
                        throw new ArgumentException();
                    keyGenerator = new KeyGenerator();
                    fileService = new FileService();
                    consoleUtils = new ConsoleUtils();
                    initialized = true;
                    return this;
                }
                catch (Exception)
                {
                    throw new ArgumentException();
                }
            }
            else
                throw new ArgumentException();

        }

        public bool isInitialized() => initialized;
    }
}
