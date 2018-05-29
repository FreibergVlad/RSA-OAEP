using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.util
{
    public class ConsoleUtils
    {
        /// <summary>
        ///     Method used to read passphrase from STDIN
        /// </summary>
        /// <returns>passphrase, instance if <see cref="SecureString"/></returns>
        public string GetPassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                    break;
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }
                Console.Write('*');
                sb.Append(cki.KeyChar);
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}
