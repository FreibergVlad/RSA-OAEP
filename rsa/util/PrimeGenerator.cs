using RSA.util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace rsa.util
{
    /// <summary>
    ///     Class that provides method to generate random prime numbers
    /// </summary>
    public class PrimeGenerator
    {

        // Number of Rabin-Miller tests
        private const int NUMBER_OF_PRIMALITY_TESTS = 128;

        // First 53 prime numbers
        private int[] firstPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                                      31, 37, 41, 43, 47, 53, 59, 61, 67,
                                      71, 73, 79, 83, 89, 97, 101, 103, 107,
                                      109, 113, 127, 131, 137, 139, 149, 151,
                                      157, 163, 167, 173, 179, 181, 191, 193,
                                      197, 199, 211, 223, 227, 229, 233, 239, 241, 251 };

        /// <summary>
        ///     Method that return random prime number of length <paramref name="bitLength"/>
        /// </summary>
        /// <param name="bitLength">Required length of number</param>
        /// <returns>Random prime number of length <paramref name="bitLength"/></returns>
        public BigInteger GetRandomPrime(int bitLength)
        {
            BigInteger res;
            do
            {
                res = MathUtils.GetRandomNumber(bitLength);
            } while (!IsPrime(res));
            return res;
        }

        /// <summary>
        ///     Method that checks number for primality
        /// </summary>
        /// <param name="n">Number we need a primality test for</param>
        /// <returns>true, if <paramref name="n"/></returns>
        public bool IsPrime(BigInteger n)
        {
            foreach (int prime in firstPrimes)
                if (n % prime == 0)
                    return false;
            return MathUtils.RabinMillerTest(n, NUMBER_OF_PRIMALITY_TESTS);
        }
    }
}
