using RSA.errors;
using RSA.keys;
using RSA.util;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSA.cipher.schemes
{
    public class RSASSA_PSS
    {
        private DataPrimitives dataPrimitives;
        private CryptoPrimitives cryptoPrimitives;
        private HashAlgorithm hash;
        private MGF mgf;
        private const int DEFAULT_SALT_LENGTH = 32;

        public RSASSA_PSS(DataPrimitives dataPrimitives, CryptoPrimitives cryptoPrimitives, HashAlgorithm hash, MGF mgf)
        {
            this.dataPrimitives = dataPrimitives;
            this.cryptoPrimitives = cryptoPrimitives;
            this.hash = hash;
            this.mgf = mgf;
        }

        public byte[] RSASSA_PSS_Sign(PrivateKey privateKey, byte[] M)
        {
            int k = privateKey.GetOctetsLength;
            byte[] EM = EMSA_PSS_Encoding(M, k * 8 - 1, DEFAULT_SALT_LENGTH);
            var m = dataPrimitives.OS2IP(M);
            var s = cryptoPrimitives.RSASP1(privateKey, m);
            return dataPrimitives.I2OSP(s, k);
        }

        public bool RSASSA_PSS_Verify(PublicKey publicKey, byte[] M, byte[] S)
        {
            int k = publicKey.GetOctetsLength;
            if (S.Length != k)
                return false;
            var s = dataPrimitives.OS2IP(S);
            var m = cryptoPrimitives.RSAVP1(publicKey, s);
            var EM = dataPrimitives.I2OSP(m, k);
            return EMSA_PSS_Verify(M, EM, k * 8 - 1);
        }

        public byte[] EMSA_PSS_Encoding(byte[] M, int k, int sLen)
        {
            int hLen = hash.HashSize;
            int emLen = k / 8;
            var mHash = hash.ComputeHash(M);
            if (emLen < hLen + sLen + 2)
                throw new EncodingException();
            byte[] salt = ByteArraysUtils.GetRandomOctets(RNGCryptoServiceProvider.Create(), sLen);
            var M_dash = ByteArraysUtils.Concat(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, ByteArraysUtils.Concat(mHash, salt));
            var H = hash.ComputeHash(M_dash);
            var PS = new byte[emLen - sLen - hLen - 2];
            var DB = ByteArraysUtils.Concat(ByteArraysUtils.Concat(PS, new byte[] {0x01}), salt);
            var dbMask = mgf.MGF1(H, emLen - hLen - 1, hash);
            var maskedDB = ByteArraysUtils.XorBytes(DB, dbMask);
            maskedDB[0] &= (byte)(0xFF >> (8 * emLen - k));
            var EM = ByteArraysUtils.Concat(ByteArraysUtils.Concat(maskedDB, H), new byte[] { 0xbc });
            return EM;
        }

        public bool EMSA_PSS_Verify(byte[] M, byte[] EM, int k)
        {
            var mHash = hash.ComputeHash(M);
            int hLen = mHash.Length;
            int emLen = EM.Length;
            return false;
        }
    }
}
