using RSA.errors;
using RSA.keys;
using RSA.util;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Linq;

namespace RSA.cipher.schemes
{
    /// <summary>
    ///     RSAES-OAEP combines the <see cref="CryptoPrimitives.RSAEP(PublicKey, BigInteger)"/> and 
    ///     <see cref="CryptoPrimitives.RSADP(PrivateKey, BigInteger)"/> primitives 
    ///     with the <see cref="EME_OAEP_Encoding(byte[], byte[], int, HashAlgorithm)"/> encoding method. 
    ///     EME-OAEP is based on Bellare and Rogaway's Optimal Asymmetric Encryption scheme [OAEP].
    ///     RSAES-OAEP can operate on messages of length up to k - 2hLen -2 octets, 
    ///     where hLen is the length of the output from the underlying hash function 
    ///     and k is the length in octets of the recipient's RSA modulus.  
    ///     See https://tools.ietf.org/html/rfc3447#page-16 for more details 
    /// </summary>
    public class RSAES_OAEP
    {

        private DataPrimitives dataPrimitives = new DataPrimitives();
        private CryptoPrimitives cryptoPrimitives = new CryptoPrimitives();
        private MGF mgf = new MGF();

        /// <summary>
        ///     Method that combines <see cref="CryptoPrimitives.RSAEP(PublicKey, BigInteger)"/>
        ///     and <see cref="EME_OAEP_Encoding(byte[], byte[], int, HashAlgorithm)"/>
        /// </summary>
        /// <param name="publicKey">Object that represents public key. Instance of <see cref="PublicKey"/></param>
        /// <param name="message">
        ///     Message to be encrypted, an octet string of length mLen, where mLen <= k - 2hLen - 2
        /// </param>
        /// <param name="label">
        ///     Optional label to be associated with the message; the
        ///     default value for L, if L is not provided, is the empty string.
        /// </param>
        /// <param name="hash">
        ///     Hash function (hLen denotes the length in octets of
        ///     the hash function output)
        /// </param>
        /// <returns>Ciphertext, an octet string of length k</returns>
        /// <exception cref="MessageTooLongException">Thrown when <paramref name="message"/>
        ///     Thrown when message length > k - 2 * hLen -2
        /// </exception>
        public byte[] RSAES_OAEP_Encrypt(PublicKey publicKey, byte[] message, byte[] label, HashAlgorithm hash)
        {
            int k = publicKey.GetOctetsLength;
            int hLen = hash.HashSize / 8;
            // TO DO LABEL LENGTH CHECK
            if (message.Length > k - 2 * hLen - 2)
                throw new MessageTooLongException("Message to long!");
            byte[] EM = EME_OAEP_Encoding(message, label, k, hash);
            BigInteger m = dataPrimitives.OS2IP(EM);
            BigInteger c = cryptoPrimitives.RSAEP(publicKey, m);
            return dataPrimitives.I2OSP(c, k);
        }

        /// <summary>
        ///     Method that combines <see cref="CryptoPrimitives.RSADP(PrivateKey, BigInteger)"/>
        ///     and <see cref="EME_OAEP_Decoding(byte[], byte[], int, HashAlgorithm)"/>
        /// </summary>
        /// <param name="privateKey">
        ///     Object that represents private key. Instance of <see cref="PrivateKey"/>
        /// </param>
        /// <param name="C">
        ///     Ciphertext, an octet string of length k
        /// </param>
        /// <param name="label">
        ///     Optional label to be associated with the message; the
        ///     default value for L, if L is not provided, is the empty string.
        /// </param>
        /// <param name="hash">
        ///     Hash function (hLen denotes the length in octets of
        ///     the hash function output)
        /// </param>
        /// <returns>
        ///     message, an octet string of length mLen, where mLen <= k - 2hLen - 2
        /// </returns>
        /// <exception cref="DecryptionErrorException">
        ///     Thrown when length of ciphertext isn't k or k < 2*hLen + 2
        /// </exception>
        /// <exception cref="DecryptionErrorException">
        ///     Thrown when there are any errors in <see cref="CryptoPrimitives.RSADP(PrivateKey, BigInteger)"/>
        /// </exception>
        public byte[] RSAES_OAEP_Decrypt(PrivateKey privateKey, byte[] C, byte[] label, HashAlgorithm hash)
        {
            int k = privateKey.GetOctetsLength;
            int hLen = hash.HashSize / 8;
            // TO DO LABEL LENGTH CHECK
            if (C.Length != k || k < 2 * hLen + 2)
                throw new DecryptionErrorException("Decryption error!");
            BigInteger c = dataPrimitives.OS2IP(C);
            BigInteger m;
            try
            {
                m = cryptoPrimitives.RSADP(privateKey, c);
            } catch(MessageRepresentativeOutOfRangeException)
            {
                throw new DecryptionErrorException("Decryption error!");
            }
            byte[] EM = dataPrimitives.I2OSP(m, k);
            byte[] M = EME_OAEP_Decoding(EM, label, k, hash);
            return M;
        }

        /// <summary>
        ///     Method that implements EME-OAEP encoding.
        /// </summary>
        /// <param name="message">
        ///     Message to be encoded, an octet string of length mLen, where mLen <= k - 2hLen - 2
        /// </param>
        /// <param name="label">
        ///     Optional label to be associated with the message; the
        ///     default value for L, if L is not provided, is the empty string.
        /// </param>
        /// <param name="k">
        ///     Size of public key modulus in octets
        /// </param>
        /// <param name="hash">
        ///     Hash function (hLen denotes the length in octets of
        ///     the hash function output)
        /// </param>
        /// <returns>
        ///     Encoded message of k length
        /// </returns>
        public byte[] EME_OAEP_Encoding(byte[] message, byte[] label, int k, HashAlgorithm hash)
        {
            int hLen = hash.HashSize / 8;
            byte[] lHash = hash.ComputeHash(label);
            byte[] PS = new byte[k - message.Length - 2*hLen - 2];
            byte[] DB = new byte[k - hLen - 1];
            System.Buffer.BlockCopy(lHash, 0, DB, 0, lHash.Length);
            System.Buffer.BlockCopy(PS, 0, DB, lHash.Length, PS.Length);
            DB[lHash.Length + PS.Length] = 0x01;
            System.Buffer.BlockCopy(message, 0, DB, lHash.Length + PS.Length + 1, message.Length);
            byte[] seed = ByteArraysUtils.GetRandomOctets(RNGCryptoServiceProvider.Create(), hLen);
            byte[] dbMask = mgf.MGF1(seed, k - hLen - 1, hash);
            byte[] maskedDB = ByteArraysUtils.XorBytes(DB, dbMask);
            byte[] seedMask = mgf.MGF1(maskedDB, hLen, hash);
            byte[] maskedSeed = ByteArraysUtils.XorBytes(seed, seedMask);
            byte[] EM = new byte[k];
            System.Buffer.SetByte(EM, 0, 0x00);
            System.Buffer.BlockCopy(maskedSeed, 0, EM, 1, maskedSeed.Length);
            System.Buffer.BlockCopy(maskedDB, 0, EM, maskedSeed.Length+1, maskedDB.Length);
            return EM;
        }

        /// <summary>
        ///     Method that implements EME-OAEP decoding
        /// </summary>
        /// <param name="EM">
        ///     Message to be decoded. An octet string of length k
        /// </param>
        /// <param name="label">
        ///     Optional label to be associated with the message; the
        ///     default value for L, if L is not provided, is the empty string.
        /// </param>
        /// <param name="k">
        ///     Size of public key modulus in octets
        /// </param>
        /// <param name="hash">
        ///     Hash function (hLen denotes the length in octets of
        ///     the hash function output)
        /// </param>
        /// <returns>
        ///     Decoded message, octet string of length mLen
        /// </returns>
        /// <exception cref="DecryptionErrorException">
        ///     Method can throw exception
        /// </exception>
        public byte[] EME_OAEP_Decoding(byte[] EM, byte[] label, int k, HashAlgorithm hash)
        {
            int hLen = hash.HashSize / 8;
            byte[] lHash = hash.ComputeHash(label);
            if (EM[0] != 0x00)
                throw new DecryptionErrorException("Decryption error!");
            byte[] maskedSeed = new byte[hLen];
            byte[] maskedDB = new byte[k - hLen - 1];
            System.Buffer.BlockCopy(EM, 1, maskedSeed, 0, maskedSeed.Length);
            System.Buffer.BlockCopy(EM, 1 + maskedSeed.Length, maskedDB, 0, maskedDB.Length);
            byte[] seedMask = mgf.MGF1(maskedDB, hLen, hash);
            byte[] seed = ByteArraysUtils.XorBytes(maskedSeed, seedMask);
            byte[] dbMask = mgf.MGF1(seed, k - hLen - 1, hash);
            byte[] DB = ByteArraysUtils.XorBytes(maskedDB, dbMask);
            byte[] lHash_dash = new byte[hLen];
            System.Buffer.BlockCopy(DB, 0, lHash_dash, 0, hLen);
            if (!lHash.SequenceEqual(lHash_dash))
                throw new DecryptionErrorException("Decryption error!");
            int i = Array.FindIndex(DB, hLen, el => el == 0x01);
            if(i == -1)
                throw new DecryptionErrorException("Decryption error!");
            byte[] M = new byte[DB.Length - 1 - i];
            System.Buffer.BlockCopy(DB, i + 1, M, 0, M.Length);
            return M;
        }
    }
}
