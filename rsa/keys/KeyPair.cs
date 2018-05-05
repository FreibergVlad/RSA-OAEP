using System;
using System.Collections.Generic;
using System.Text;

namespace RSA.keys
{
    /// <summary>
    ///     Class that represents RSA key pair
    /// </summary>
    public class KeyPair
    {        
        private PrivateKey privateKey;
        private PublicKey publicKey;

        public KeyPair(PublicKey publicKey, PrivateKey privateKey)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        public PrivateKey GetPrivateKey { get => privateKey; }

        public PublicKey GetPublicKey { get => publicKey; }

        public override bool Equals(object obj)
        {
            var pair = obj as KeyPair;
            return pair != null &&
                   EqualityComparer<PrivateKey>.Default.Equals(privateKey, pair.privateKey) &&
                   EqualityComparer<PublicKey>.Default.Equals(publicKey, pair.publicKey) &&
                   EqualityComparer<PrivateKey>.Default.Equals(GetPrivateKey, pair.GetPrivateKey) &&
                   EqualityComparer<PublicKey>.Default.Equals(GetPublicKey, pair.GetPublicKey);
        }

        public override int GetHashCode()
        {
            var hashCode = -670443348;
            hashCode = hashCode * -1521134295 + EqualityComparer<PrivateKey>.Default.GetHashCode(privateKey);
            hashCode = hashCode * -1521134295 + EqualityComparer<PublicKey>.Default.GetHashCode(publicKey);
            hashCode = hashCode * -1521134295 + EqualityComparer<PrivateKey>.Default.GetHashCode(GetPrivateKey);
            hashCode = hashCode * -1521134295 + EqualityComparer<PublicKey>.Default.GetHashCode(GetPublicKey);
            return hashCode;
        }
    }
}
