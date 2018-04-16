using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSA.util
{
    public class ByteArraysUtils
    {
        public static byte[] GetRandomNonZeroOctets(RandomNumberGenerator rnd, int length)
        {
            byte[] res = new byte[length];
            rnd.GetNonZeroBytes(res);
            return res;
        }

        public static byte[] GetRandomOctets(RandomNumberGenerator rnd, int len)
        {
            byte[] res = new byte[len];
            rnd.GetBytes(res);
            return res;
        }

        public static byte[] Concat(byte[] arr1, byte[] arr2)
        {
            byte[] res = new byte[arr1.Length + arr2.Length];
            System.Buffer.BlockCopy(arr1, 0, res, 0, arr1.Length);
            System.Buffer.BlockCopy(arr2, 0, res, arr1.Length, arr2.Length);
            return res;
        }

        public static byte[] GetSubArray(byte[] arr, int offSet, int count)
        {
            byte[] res = new byte[count];
            System.Buffer.BlockCopy(arr, offSet, res, 0, count);
            return res;
        }

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
