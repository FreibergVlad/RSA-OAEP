using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class DecryptionErrorException : ArgumentException
    {
        public DecryptionErrorException(string message) : base(message)
        {
        }
    }
}
