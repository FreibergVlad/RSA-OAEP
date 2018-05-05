using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSA.util
{
    /// <summary>
    ///     Class that provides methods to manipulate byte arrays
    /// </summary>
    public class ByteArraysUtils
    {
        /// <summary>
        ///     Method that return random non-zero byte array of length <<paramref name="length"/> 
        /// </summary>
        /// <param name="rnd">Random number generator. Instance of <see cref="RandomNumberGenerator"/></param>
        /// <param name="length">Required length of the random byte array</param>
        /// <returns>Byte array of length <paramref name="length"/></returns>
        public static byte[] GetRandomNonZeroOctets(RandomNumberGenerator rnd, int length)
        {
            byte[] res = new byte[length];
            rnd.GetNonZeroBytes(res);
            return res;
        }

        /// <summary>
        ///     Method that return random byte array of length <<paramref name="len"/> 
        /// </summary>
        /// <param name="rnd">Random number generator. Instance of <see cref="RandomNumberGenerator"/></param>
        /// <param name="len">Required length of the random byte array</param>
        /// <returns>Byte array of length <paramref name="length"/></returns>
        public static byte[] GetRandomOctets(RandomNumberGenerator rnd, int len)
        {
            byte[] res = new byte[len];
            rnd.GetBytes(res);
            return res;
        }

        /// <summary>
        ///     Method that concatenate two byte arrays
        /// </summary>
        /// <param name="arr1">Byte array</param>
        /// <param name="arr2">Byte array</param>
        /// <returns>New byte array of length arr1.Length + arr2.Length</returns>
        public static byte[] Concat(byte[] arr1, byte[] arr2)
        {
            byte[] res = new byte[arr1.Length + arr2.Length];
            System.Buffer.BlockCopy(arr1, 0, res, 0, arr1.Length);
            System.Buffer.BlockCopy(arr2, 0, res, arr1.Length, arr2.Length);
            return res;
        }

        /// <summary>
        ///     Method that return subarray of <paramref name="arr"/> of length <paramref name="count"/>
        ///     starting from index <paramref name="offSet"/>
        /// </summary>
        /// <param name="arr">Byte array whose subarray we need to</param>
        /// <param name="offSet">Index of <paramref name="arr"/> we start from</param>
        /// <param name="count">Number of elements we need in subarray</param>
        /// <returns></returns>
        public static byte[] GetSubArray(byte[] arr, int offSet, int count)
        {
            byte[] res = new byte[count];
            System.Buffer.BlockCopy(arr, offSet, res, 0, count);
            return res;
        }

        /// <summary>
        ///     Method that apply XOR to the two byte arrays
        /// </summary>
        /// <param name="arr1">Byte array, first operand</param>
        /// <param name="arr2">Byte array, second operand</param>
        /// <returns>Byte array, result of XOR</returns>
        public static byte[] XorBytes(byte[] arr1, byte[] arr2)
        {
            BitArray bitArr1 = new BitArray(arr1);
            BitArray bitArr2 = new BitArray(arr2);
            byte[] res = new byte[arr1.Length];
            (bitArr1.Xor(bitArr2)).CopyTo(res, 0);
            return res;
        }
    }
}
