using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.command
{
    /// <summary>
    ///     Implementation of <see cref="Command"/> interface. 
    ///     Class that represents --help console command.
    ///     Command prints usage message in standard output.
    /// </summary>
    public class HelpCommand : Command
    {

        public void Execute()
        {
            ShowUsage();
        }

        public Command Initialize(string[] args)
        {
            return this;
        }

        public bool isInitialized() => true;

        private void ShowUsage()
        {
            Console.WriteLine("DESCRIPTION");
            Console.WriteLine(" rsa - encryption and sign tool. Simple implementaton of PKCS#1 v2.2 standard, created for educational purposes.");
            Console.WriteLine("COMMANDS");
            Console.WriteLine(" --gen-keys key_size path_to_keys");
            Console.WriteLine(" --encrypt public_key file enc_file");
            Console.WriteLine(" --decrypt private_key file");
            Console.WriteLine(" --sign private_key file");
            Console.WriteLine(" --verify_sign public_key file");
        }
    }
}
