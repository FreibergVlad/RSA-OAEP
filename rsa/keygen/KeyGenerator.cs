using rsa.util;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA.keygen
{
    /// <summary>
    ///     Class for creating private and public keys
    /// </summary>
    public class KeyGenerator
    {
        private PrimeGenerator primeGenerator;
        private const int PUBLIC_EXPONENT = 65537;

        public KeyGenerator()
        {
            primeGenerator = new PrimeGenerator();
        }
        
        /// <summary>
        ///     Method for generating key pair using RSA algorithm
        /// </summary>
        /// <param name="bitLength">Bit size of key</param>
        /// <returns>
        ///     Key pair. Instance of <see cref="KeyPair"/> class. Consists of
        ///     public and private keys.
        /// </returns>
        public KeyPair GenerateKeys(int bitLength)
        {
            // Generate two big random primes
            BigInteger p = primeGenerator.GetRandomPrime(bitLength / 2);
            BigInteger q = primeGenerator.GetRandomPrime(bitLength / 2);
            // Calculate the modulus
            BigInteger n = p * q;
            // Calculate the Eiler's function 
            BigInteger phi = (p - 1) * (q - 1);
            // Calculate the modular multiplicative inverse
            BigInteger d = MathUtils.ModInverse(PUBLIC_EXPONENT, phi);
            // The public key consists of the modulus n and the public exponent e
            PublicKey publicKey = new PublicKey(n, PUBLIC_EXPONENT);
            //The private key consists of the private exponent d, which must be kept secret, and modulus n
            PrivateKey privateKey = new PrivateKey(n, d);
            return new KeyPair(publicKey, privateKey);
        }


    }
}
