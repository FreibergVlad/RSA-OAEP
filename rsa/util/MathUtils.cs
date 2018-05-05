using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace RSA.util
{
    /// <summary>
    ///     Math utility class with statis methods
    /// </summary>
    public class MathUtils
    {
        /// <summary>
        ///     Method that returns modular inverse of a (mod m),
        ///     using extended Euclidean algorithm.
        /// </summary>
        /// <param name="a">Number</param>
        /// <param name="m">Modulus</param>
        /// <returns>Mod inverse of a (mod m)</returns>
        public static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;
            if (m == 1)
                return 0;
            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger t = m;
                m = a % m;
                a = t;
                t = y;
                y = x - q * y;
                x = t;
            }
            if (x < 0)
                x += m0;
            return x;
        }

        /// <summary>
        ///     The primality test that provides an efficient probabilistic algorithm
        ///     for determining if a given number is prime. It based on the properties of
        ///     strong pseudoprimes.
        /// </summary>
        /// <param name="n">Number we need a primality test for</param>
        /// <param name="k">A parameter that determines the accuracy of the test</param>
        /// <returns>true, if number is probably prime, false otherwise</returns>
        public static bool RabinMillerTest(BigInteger n, int k)
        {
            if (n <= 2) return n == 2;
            var s = 0;
            var t = n - 1;
            while (t % 2 == 0)
            {
                t /= 2;
                s++;
            }
            var rnd = RandomNumberGenerator.Create();
            for (var i = 0; i < k; i++)
            {
                var a = GetRandomNumber(rnd, 2, n - 2);
                var x = BigInteger.ModPow(a, t, n);
                if (x == 1 || x == n - 1)
                    continue;
                for (var j = 0; j < s - 1; j++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }
                if (x == n - 1)
                    continue;
                return false;
            }
            return true;
        }

        /// <summary>
        ///     Method that return random number of required length
        /// </summary>
        /// <param name="bitLength">Required length of the number in bits</param>
        /// <returns>The number of <paramref name="bitLength"/> length</returns>
        public static BigInteger GetRandomNumber(int bitLength)
        {
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[bitLength / 8];
            rnd.GetBytes(bytes);
            return BigInteger.Abs(new BigInteger(bytes));
        }

        /// <summary>
        ///     Method that returns random number in range [min; max]
        /// </summary>
        /// <param name="rnd">Random number generator. Instance of <see cref="RandomNumberGenerator"/></param>
        /// <param name="min">Lower bound of range</param>
        /// <param name="max">Upper bound of range</param>
        /// <returns>Random number in range [min; max]. Instance of <see cref="BigInteger"/></returns>
        private static BigInteger GetRandomNumber(RandomNumberGenerator rnd, BigInteger min, BigInteger max)
        {
            if (min > max)
            {
                var temp = max;
                max = min;
                min = temp;
            }
            var offset = -min;
            min = 0;
            max += offset;
            var value = GetRandomNumber(rnd, max) - offset;
            return value;
        }

        /// <summary>
        ///     Method that returns random number in range [0; max]
        /// </summary>
        /// <param name="rnd">Random number generator. Instance of <see cref="RandomNumberGenerator"/></param>
        /// <param name="max">Upper bound of range</param>
        /// <returns>Random number in range [0; max]. Instance of <see cref="BigInteger"/></returns>
        private static BigInteger GetRandomNumber(RandomNumberGenerator rnd, BigInteger max)
        {
            BigInteger value;
            var bytes = max.ToByteArray();
            byte zeroBitMask = 0b00000000;
            byte msByte = bytes[bytes.Length - 1];
            for (var i = 7; i >= 0; i--)
            {
                if ((msByte & (0b1 << i)) != 0)
                {
                    var zeroBits = 7 - i;
                    zeroBitMask = (byte)(0b11111111 >> zeroBits);
                    break;
                }
            }
            do
            {
                rnd.GetBytes(bytes);
                bytes[bytes.Length - 1] &= zeroBitMask;
                value = new BigInteger(bytes);
            } while (value > max);
            return value;
        }
    }
}
