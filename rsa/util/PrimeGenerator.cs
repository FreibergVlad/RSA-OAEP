using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace rsa.util
{
    public class PrimeGenerator
    {

        private const int NUMBER_OF_PRIMALITY_TESTS = 128;

        private int[] firstPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                                      31, 37, 41, 43, 47, 53, 59, 61, 67,
                                      71, 73, 79, 83, 89, 97, 101, 103, 107,
                                      109, 113, 127, 131, 137, 139, 149, 151,
                                      157, 163, 167, 173, 179, 181, 191, 193,
                                      197, 199, 211, 223, 227, 229, 233, 239, 241, 251 };

        public BigInteger GetRandomPrime(int bitLength)
        {
            BigInteger res;
            do
            {
                res = GetRandomNumber(bitLength);
            } while (!IsPrime(res));
            return res;
        }

        public bool IsPrime(BigInteger n)
        {
            foreach (int prime in firstPrimes)
                if (n % prime == 0)
                    return false;
            return RabinMillerTest(n, NUMBER_OF_PRIMALITY_TESTS);
        }

        private bool RabinMillerTest(BigInteger n, int k)
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
            for (var i = 0; i<k; i++)
            {
                var a = GetRandomNumber(rnd, 2, n - 2);
                var x = BigInteger.ModPow(a, t, n);
                if (x == 1 || x == n - 1)
                    continue;
                for(var j = 0; j<s-1; j++)
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

        public BigInteger GetRandomNumber(int bitLength)
        {
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[bitLength / 8];
            rnd.GetBytes(bytes);
            return BigInteger.Abs(new BigInteger(bytes));
        }

        private BigInteger GetRandomNumber(RandomNumberGenerator rnd, BigInteger min, BigInteger max)
        {
            if(min > max)
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

        private BigInteger GetRandomNumber(RandomNumberGenerator rnd, BigInteger max)
        {
            BigInteger value;
            var bytes = max.ToByteArray();
            byte zeroBitMask = 0b00000000;
            byte msByte = bytes[bytes.Length - 1];
            for(var i = 7; i>=0; i--)
            {
                if((msByte & (0b1 << i))!=0)
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
