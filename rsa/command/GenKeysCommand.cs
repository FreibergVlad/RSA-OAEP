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
        private KeyGenerator keyGenerator;
        private FileService fileService;
        private int keySize;

        public void Execute()
        {
            Console.WriteLine("Generating RSA " + keySize + " bits key pair...");
            KeyPair keyPair = keyGenerator.GenerateKeys(keySize);
            Console.WriteLine("Saving key pair in... " + AppDomain.CurrentDomain.BaseDirectory);
            fileService.SavePublicKeyInFile(keyPair.GetPublicKey, "id_rsa.pub");
            fileService.SavePrivateKeyInFile(keyPair.GetPrivateKey, "id_rsa");
            Console.WriteLine("Key pair were saved in " + AppDomain.CurrentDomain.BaseDirectory);
        }

        public Command Initialize(String[] args)
        {
            if (args.Length == 2)
            {
                try
                {
                    keySize = Convert.ToInt32(args[1]);
                    if (keySize < 2048)
                        throw new ArgumentException();
                    keyGenerator = new KeyGenerator();
                    fileService = new FileService();
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
    }
}
