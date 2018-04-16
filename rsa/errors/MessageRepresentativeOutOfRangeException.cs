using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    class MessageRepresentativeOutOfRangeException : ArgumentException
    {
        public MessageRepresentativeOutOfRangeException(string message) : base(message)
        {
        }
    }
}
