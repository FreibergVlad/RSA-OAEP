using RSA.errors;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA.cipher
{
    /// <summary>
    ///     Two data conversion primitives are employed in the schemes implemented 
    ///     in this class:
    ///         I2OSP - Integer-To-Octet-String Primitive
    ///         OS2IP - Octet-String-To-Integer Primitive
    ///     See https://tools.ietf.org/html/rfc8017#section-4 for more details
    /// </summary>
    public class DataPrimitives
    {
        /// <summary>
        ///     I2OSP converts a nonnegative integer to an octet string of a
        ///     specified length.
        /// </summary>
        /// <param name="x">Nonnegative integer to be converted</param>
        /// <param name="xLen">Intended length of the resulting octet string</param>
        /// <returns>
        ///     Corresponding octet string of length xLen.
        /// </returns>
        /// <exception cref="IntegerTooLargeException">
        ///     Thrown when <paramref name="x"/> >= 256^<paramref name="xLen"/>
        /// </exception>
        public byte[] I2OSP(BigInteger x, int xLen)
        {
            if (x >= BigInteger.Pow(256, xLen))
                throw new IntegerTooLargeException("Integer too large");
            byte[] bytes = new byte[xLen];
            BigInteger cur = x;
            for (int i = 1; i <= xLen; i++)
            {
                var num = BigInteger.Pow(256, xLen - i);
                bytes[i-1] = (byte) (cur/num);
                cur %= num;
            }
            return bytes;
        }

        /// <summary>
        ///     OS2IP converts an octet string to a nonnegative integer.
        /// </summary>
        /// <param name="X">Octet string to be converted</param>
        /// <returns>Corresponding nonnegative integer</returns>
        public BigInteger OS2IP(byte[] X)
        {
            BigInteger res = 0;
            for(int i = 1; i<=X.Length; i++)
                res += (0xFF & X[i-1]) * BigInteger.Pow(256, X.Length-i);
            return res;
        }
    }
}
 