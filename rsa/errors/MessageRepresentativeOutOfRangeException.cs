using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class MessageRepresentativeOutOfRangeException : DecryptionException
    {
        public MessageRepresentativeOutOfRangeException(string message) : base(message)
        {
        }
    }
}
