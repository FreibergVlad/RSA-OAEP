using RSA.errors;
using RSA.keys;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA.cipher
{
    /// <summary>
    ///     Cryptographic primitives are basic mathematical operations on which
    ///     cryptographic schemes can be built. Four types of primitive are implemented in this class, organized in
    ///     pairs: encryption and decryption; and signature and verification.
    ///     See https://tools.ietf.org/html/rfc8017#section-5 for more details.
    /// </summary>
    public class CryptoPrimitives
    {

        /// <summary>
        ///     An encryption primitive produces a ciphertext representative from a
        ///     message representative under the control of a public key
        /// </summary>
        /// <param name="publicKey">Object that represents Public Key. Instance of <see cref="PublicKey"/></param>
        /// <param name="m">Message representative, an integer between 0 and n - 1, where n is a public key modulus</param>
        /// <returns>
        ///     Ciphertext representative, an integer between 0 and n - 1
        /// </returns>
        /// <exception cref="MessageRepresentativeOutOfRangeException">
        ///     Thrown when <paramref name="m"/> is not between 0 and n-1
        /// </exception>
        public BigInteger RSAEP(PublicKey publicKey, BigInteger m)
        {
            BigInteger n = publicKey.GetModulus;
            BigInteger e = publicKey.GetPublicExponent;
            if (m < 0 || m > n - 1)
                throw new MessageRepresentativeOutOfRangeException("Message representative out of range!");
            return BigInteger.ModPow(m, e, n);
        }

        /// <summary>
        ///  Decryption primitive recovers the message representative from the
        ///  ciphertext representative under the control of the corresponding
        ///  private key.
        /// </summary>
        /// <param name="privateKey">Object that represents private key. Instance of <see cref="PrivateKey"/></param>
        /// <param name="c">Ciphertext representative, an integer between 0 and n - 1</param>
        /// <returns>
        ///     Message representative, an integer between 0 and n - 1
        /// </returns>
        /// <exception cref="MessageRepresentativeOutOfRangeException">
        ///     Thrown when <paramref name="c"/> is not between 0 and n-1
        /// </exception>
        public BigInteger RSADP(PrivateKey privateKey, BigInteger c)
        {
            BigInteger n = privateKey.GetModulus;
            BigInteger d = privateKey.GetPrivateExponent;
            if (c < 0 || c > n - 1)
                throw new MessageRepresentativeOutOfRangeException("Ciphertext representative out of range!");
            return BigInteger.ModPow(c, d, n);
        }

        /// <summary>
        ///     A signature primitive produces a signature representative from a
        ///     message representative under the control of a private key.
        /// </summary>
        /// <param name="privateKey">Object that represents private key. Instance of <see cref="PrivateKey"/></param>
        /// <param name="m">Message representative, an integer between 0 and n - 1</param>
        /// <returns>Signature representative, an integer between 0 and n - 1</returns>
        /// <exception cref="MessageRepresentativeOutOfRangeException">
        ///     Thrown when <paramref name="m"/> is not between 0 and n-1
        /// </exception>
        public BigInteger RSASP1(PrivateKey privateKey, BigInteger m)
        {
            BigInteger n = privateKey.GetModulus;
            BigInteger d = privateKey.GetPrivateExponent;
            if (m < 0 || m > n - 1)
                throw new MessageRepresentativeOutOfRangeException("Message representative out of range!");
            return BigInteger.ModPow(m, d, n);
        }

        /// <summary>
        ///     Verification primitive recovers the message representative from the
        ///     signature representative under the control of the corresponding
        ///     public key.
        /// </summary>
        /// <param name="publicKey">Object that represents Public Key. Instance of <see cref="PublicKey"/></param>
        /// <param name="s">Signature representative, an integer between 0 and n - 1</param>
        /// <returns>Message representative, an integer between 0 and n - 1</returns>
        /// <exception cref="MessageRepresentativeOutOfRangeException">
        ///     Thrown when <paramref name="s"/> is not between 0 and n-1
        /// </exception>
        public BigInteger RSAVP1(PublicKey publicKey, BigInteger s)
        {
            BigInteger n = publicKey.GetModulus;
            BigInteger e = publicKey.GetPublicExponent;
            if (s < 0 || s > n - 1)
                throw new MessageRepresentativeOutOfRangeException("Signature representative out of range!");
            return BigInteger.ModPow(s, e, n);
        }
    }
}
