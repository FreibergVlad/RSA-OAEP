using rsa.util;
using RSA.cipher.schemes;
using RSA.keygen;
using RSA.keys;
using RSA.util;
using System;
using System.Security.Cryptography;
using System.Text;

namespace rsa
{
    class Program
    {
        static void Main(string[] args)
        {
            
         
        }

        private static void ShowUsage()
        {
            Console.WriteLine("DESCRIPTION");
            Console.WriteLine(" rsa - encryption and sign tool. Simple implementaton of PKCS#1 v2.2 standard, created for educational purposes.");
            Console.WriteLine("COMMANDS");
            Console.WriteLine(" --gen-keys key_size path_to_keys prefix");
            Console.WriteLine(" --encrypt public_key file");
            Console.WriteLine(" --decrypt private_key file");
            Console.WriteLine(" --sign private_key file");
            Console.WriteLine(" --verify_sign public_key file");
        }
    }
}
