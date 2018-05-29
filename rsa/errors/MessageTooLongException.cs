using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class MessageTooLongException : DecryptionException
    {
        public MessageTooLongException(string message) : base(message)
        {
        }
    }
}
