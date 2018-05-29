using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class DecryptionErrorException : DecryptionException
    {
        public DecryptionErrorException(string message) : base(message)
        {
        }
    }
}
