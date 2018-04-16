using RSA.errors;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace RSA.cipher.schemes
{
    public class RSAES_PKCS1
    {

        private DataPrimitives dataPrimitives;
        private CryptoPrimitives cryptoPrimitives;

        public RSAES_PKCS1()
        {
            dataPrimitives = new DataPrimitives();
            cryptoPrimitives = new CryptoPrimitives();
        }

        public byte[] RSAES_PKCS1_Encrypt(PublicKey publicKey, byte[] M)
        {
            int k = publicKey.GetOctetsLength;
            if (M.Length > k - 11)
                throw new MessageTooLongException("Message too long");
            byte[] EM = EME_PKCS1_Encoding(k, M);
            BigInteger m = dataPrimitives.OS2IP(EM);
            BigInteger c = cryptoPrimitives.RSAEP(publicKey, m);
            byte[] C = dataPrimitives.I2OSP(c, k);
            return C;
        }

        public byte[] RSAES_PKCS1_Decrypt(PrivateKey privateKey, byte[] C)
        {
            int k = privateKey.GetOctetsLength;
            if (C.Length != k)
                throw new DecryptionErrorException("Decryption error");
            BigInteger c = dataPrimitives.OS2IP(C);
            BigInteger m;
            try
            {
                m = cryptoPrimitives.RSADP(privateKey, c);
            } catch(MessageRepresentativeOutOfRangeException e)
            {
                throw new DecryptionErrorException("Decryption error");
            }
            byte[] EM = dataPrimitives.I2OSP(m, k);
            byte[] M = EME_PKCS1_Decoding(EM);
            return M;
        }

        public byte[] EME_PKCS1_Encoding(int k, byte[] M)
        {
            byte[] PS = ByteArraysUtils.GetRandomNonZeroOctets(RNGCryptoServiceProvider.Create(), k - M.Length - 3);
            byte[] EM = new byte[k];
            System.Buffer.SetByte(EM, 0, (byte)0x00);
            System.Buffer.SetByte(EM, 1, (byte)0x02);
            System.Buffer.BlockCopy(PS, 0, EM, 2, PS.Length);
            System.Buffer.SetByte(EM, PS.Length + 2, (byte)0x00);
            System.Buffer.BlockCopy(M, 0, EM, PS.Length + 3, M.Length);
            return EM;
        }

        public byte[] EME_PKCS1_Decoding(byte[] EM)
        {
            if (EM[0] != (byte)0x00 || EM[1] != (byte)0x02)
                throw new DecryptionErrorException("Decryption error");
            int i = Array.FindIndex(EM, 2, el => el == 0x00);
            if (i == -1 || i - 2 < 8)
                throw new DecryptionErrorException("Decryption error");
            byte[] M = new byte[EM.Length - 1 - i];
            System.Buffer.BlockCopy(EM, i+1, M, 0, M.Length);
            return M;
        }
    }
}
