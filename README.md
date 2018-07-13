# Fvcrypt
fvcrypt - simple cryptography tool, created for educational purposes. It's a simplifyed implementation of [PKCS #1 v.2.2](https://tools.ietf.org/html/rfc8017) standard. <br>
Program provides command line interface that implements the following functions:
<ul>
  <li>Generation of RSA key pair</li>
  <li>RSA encryption with OAEP encoding method</li>
  <li>RSA encryption with PKCS1-v1_5 encoding method</li>
  <li>Private key encryption with AES-256</li>
</ul>

### Usage

<ul>
  <li>Key Generation <code> --gen-keys [key size] </code></li>
  <li>Encryption <code> --encrypt [public key] [file to encrypt] [name of encrypted file] </code></li>
  <li>Decryption <code> --decrypt [private key] [file to decrypt] [name of decrypted file] </code></li>
  <li>Help <code> --help </code></li>
</ul>
