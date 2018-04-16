using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.errors
{
    public class MaskTooLongException : ArgumentException
    {
        public MaskTooLongException(string message) : base(message)
        {
        }
    }
}
