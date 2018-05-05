using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA.keys
{
    public class PrivateKey : Key
    {
        private BigInteger modulus;
        private BigInteger privateExponent;
        private int octetsLength;

        public PrivateKey(BigInteger modulus, BigInteger privateExponent)
        {
            this.modulus = modulus;
            this.privateExponent = privateExponent;
            octetsLength = modulus.ToByteArray().Length;
        }

        public BigInteger GetModulus { get => modulus; }

        public BigInteger GetPrivateExponent { get => privateExponent; }

        public int GetOctetsLength { get => octetsLength; }

        public override string ToString()
        {
            string res = "modulus:" + Environment.NewLine + modulus + Environment.NewLine;
            res = res + "private exponent:" + Environment.NewLine + privateExponent;
            return res;
        }

        public override bool Equals(object obj)
        {
            var key = obj as PrivateKey;
            return key != null &&
                   modulus.Equals(key.modulus) &&
                   privateExponent.Equals(key.privateExponent) &&
                   GetModulus.Equals(key.GetModulus) &&
                   GetPrivateExponent.Equals(key.GetPrivateExponent);
        }

        public override int GetHashCode()
        {
            var hashCode = -455559950;
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(modulus);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(privateExponent);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(GetModulus);
            hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(GetPrivateExponent);
            return hashCode;
        }
    }
}
