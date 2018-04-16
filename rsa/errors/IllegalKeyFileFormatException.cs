using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    class IllegalKeyFileFormatException : FormatException
    {
        public IllegalKeyFileFormatException(string message) : base(message)
        {
        }
    }
}
