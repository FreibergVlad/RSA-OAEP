using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA.keys
{
    public class PublicKey : Key
    {
        private BigInteger modulus;
        private BigInteger publicExponent;
        private int octetsLength;

        public PublicKey(BigInteger modulus, BigInteger publicExponent)
        {
            this.modulus = modulus;
            this.publicExponent = publicExponent;
            octetsLength = modulus.ToByteArray().Length;
        }

        public BigInteger GetModulus { get => modulus; }

        public BigInteger GetPublicExponent { get => publicExponent; }

        public int GetOctetsLength { get => octetsLength; }

        public override bool Equals(object obj)
        {
            var key = obj as PublicKey;
            return key != null &&
                   modulus.Equals(key.modulus) &&
                   publicExponent.Equals(key.publicExponent) &&
                   GetModulus.Equals(key.GetModulus) &&
                   GetPublicExponent.Equals(key.GetPublicExponent);
        }

        public override int GetHashCode()
        {
            var hashCode = 96773590;
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(modulus);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(publicExponent);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(GetModulus);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(GetPublicExponent);
            return hashCode;
        }
    }
}
