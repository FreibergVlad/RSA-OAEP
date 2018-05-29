using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class DecryptionException : Exception
    {
        public DecryptionException(string message) : base(message)
        {
        }
    }
}
