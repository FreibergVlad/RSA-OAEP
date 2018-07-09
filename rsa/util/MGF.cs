using RSA.cipher;
using RSA.errors;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace RSA.util
{
    /// <summary>
    ///     A mask generation function takes an octet string of variable length
    ///     and a desired output length as input, and outputs an octet string of
    ///     the desired length.
    ///     See https://tools.ietf.org/html/rfc3447#page-54 for more details.
    /// </summary>
    public class MGF
    {

        private DataPrimitives dataPrimitives = new DataPrimitives();

        /// <summary>
        ///     MGF1 is a mask generation function, based on a
        ///     hash function. MGF1 coincides with the mask generation functions
        ///     defined in IEEE Std 1363-2000 and the draft ANSI X9.44.
        /// </summary>
        /// <param name="mgfSeed">Seed from which mask is generated, an octet string</param>
        /// <param name="maskLen">Intended length in octets of the mask, at most 2^32 hLen</param>
        /// <param name="hash">
        ///     Hash function (hLen denotes the length in octets of the hash
        ///     function output)
        /// </param>
        /// <returns>An octet string of length maskLen</returns>
        /// <exception cref="MaskTooLongException">
        ///     Thrown when maskLen > 2^32*hLen
        /// </exception>
        public byte[] MGF1(byte[] mgfSeed, int maskLen, HashAlgorithm hash)
        {
            int hLen = hash.HashSize / 8;
            if (maskLen > BigInteger.Pow(2, 32) * hLen)
                throw new MaskTooLongException("Mask too long");
            byte[] T = new byte[0];
            for (BigInteger counter = 0; counter < (maskLen - 1) / hLen + 1; counter++)
            {
                byte[] C = dataPrimitives.I2OSP(counter, 4);
                byte[] H = hash.ComputeHash(ByteArraysUtils.Concat(mgfSeed, C));
                T = ByteArraysUtils.Concat(T, H);
            }
            return ByteArraysUtils.GetSubArray(T, 0, maskLen);
        }
    }
}
