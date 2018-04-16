using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    class MessageTooLongException : ArgumentException
    {
        public MessageTooLongException(string message) : base(message)
        {
            
        }
    }
}
