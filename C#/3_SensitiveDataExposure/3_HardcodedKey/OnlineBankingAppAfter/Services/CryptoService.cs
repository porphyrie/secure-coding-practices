using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBankingApp.Services
{
    public class CryptoService : ICryptoService
    {
        private const int NonceSize = 12; // GCM nonce size (96 bits)
        private const int TagSize = 16; // GCM tag size (128 bits), can be adjusted if necessary
        private const int KeySize = 32; // AES-256 key size (256 bits)
        private const int SaltSize = 16; // Recommended salt size (128 bits)
        private const int Iterations = 10000; // PBKDF2 iteration count

        public string Encrypt(string strToEncrypt, string passphrase)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize); // Generate a random salt
            byte[] key = DeriveKey(passphrase, salt); // Derive key using PBKDF2

            byte[] plaintext = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] ciphertext = new byte[plaintext.Length];
            byte[] nonce = new byte[NonceSize];
            RandomNumberGenerator.Fill(nonce); // Securely generate nonce
            byte[] tag = new byte[TagSize];

            using (var aesGcm = new AesGcm(key, TagSize))
            {
                aesGcm.Encrypt(nonce, plaintext, ciphertext, tag);
            }

            // Combine salt, nonce, ciphertext, and tag into a single byte array
            using (var memoryStream = new MemoryStream())
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                binaryWriter.Write(salt);
                binaryWriter.Write(nonce);
                binaryWriter.Write(ciphertext);
                binaryWriter.Write(tag);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public string Decrypt(string strEncrypted, string passphrase)
        {
            byte[] encryptedData = Convert.FromBase64String(strEncrypted);

            using (var memoryStream = new MemoryStream(encryptedData))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                byte[] salt = binaryReader.ReadBytes(SaltSize);
                byte[] nonce = binaryReader.ReadBytes(NonceSize);
                byte[] ciphertext = binaryReader.ReadBytes(encryptedData.Length - SaltSize - NonceSize - TagSize);
                byte[] tag = binaryReader.ReadBytes(TagSize);

                byte[] key = DeriveKey(passphrase, salt); // Derive key using the same passphrase and salt

                using (var aesGcm = new AesGcm(key, TagSize))
                {
                    byte[] plaintext = new byte[ciphertext.Length];
                    aesGcm.Decrypt(nonce, ciphertext, tag, plaintext);
                    return Encoding.UTF8.GetString(plaintext);
                }
            }
        }

        private static byte[] DeriveKey(string passphrase, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KeySize); // Derive a 256-bit key
            }
        }
    }

    public interface ICryptoService
    {
        string Encrypt(string strToEncrypt, string passphrase);
        string Decrypt(string strEncrypted, string passphrase);
    }
}
