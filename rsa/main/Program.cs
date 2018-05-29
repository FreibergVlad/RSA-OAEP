using rsa.util;
using RSA.cipher.schemes;
using RSA.command;
using RSA.errors;
using RSA.keygen;
using RSA.keys;
using RSA.util;
using System;
using System.Security.Cryptography;
using System.Text;

namespace rsa
{
    public class Program
    {

        private static CommandContainer commandContainer = new CommandContainer();

        static Program()
        {
            InitializeApplicationCommands();
        }

        static void Main(string[] args)
        {
            InitializeApplicationCommands();
            if (args.Length >= 1 && commandContainer.ContainsCommand(args[0]))
            {
                try
                {
                    commandContainer.GetCommand(args[0]).Initialize(args).Execute();
                }
                catch (CryptographicException)
                {
                    Console.WriteLine("Incorrect passphrase!");
                    Environment.Exit(1);
                }
                catch (DecryptionException)
                {
                    Console.WriteLine("Decryption error!");
                    Environment.Exit(1);
                }
                catch(Exception)
                {
                    Console.WriteLine("Incorrect usage!");
                    commandContainer.GetCommand("--help").Execute();
                    Environment.Exit(1);
                }
            } else
            {
                Console.WriteLine("Incorrect usage!");
                commandContainer.GetCommand("--help").Execute();
                Environment.Exit(1);
            }
        }

        private static void InitializeApplicationCommands()
        {
            commandContainer.AddCommand("--help", new HelpCommand());
            commandContainer.AddCommand("--gen-keys", new GenKeysCommand());
            commandContainer.AddCommand("--encrypt", new EncryptCommand());
            commandContainer.AddCommand("--decrypt", new DecryptCommand());
        }
    }
}
