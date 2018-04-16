using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class IntegerTooLargeException : ArgumentException
    {
        public IntegerTooLargeException(string message) : base(message)
        {
        }
    }
}
