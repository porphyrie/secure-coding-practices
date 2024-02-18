using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class Argon2PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    public string HashPassword(TUser user, string password)
    {
        // Create a new Argon2id instance
        using (var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password)))
        {
            // Configure the Argon2 parameters
            argon2.Salt = GenerateSalt(); // Generate a random salt
            argon2.DegreeOfParallelism = 8; // Number of threads
            argon2.MemorySize = 1024 * 1024; // Amount of memory to use (1 GB)
            argon2.Iterations = 4; // Number of iterations

            // Perform the hash
            var hashBytes = argon2.GetBytes(128); // Size of the hash
            var saltAndHash = new byte[argon2.Salt.Length + hashBytes.Length];
            argon2.Salt.CopyTo(saltAndHash, 0);
            hashBytes.CopyTo(saltAndHash, argon2.Salt.Length);
            return Convert.ToBase64String(saltAndHash);
        }
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        // Extract the salt from the stored hash
        var saltAndHashBytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[128 / 8]; // Assuming a 128-bit salt
        Array.Copy(saltAndHashBytes, 0, salt, 0, salt.Length);

        // Extract the hash from the stored hash
        var hashBytes = new byte[saltAndHashBytes.Length - salt.Length];
        Array.Copy(saltAndHashBytes, salt.Length, hashBytes, 0, hashBytes.Length);

        // Hash the provided password with the same salt
        using (var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(providedPassword)))
        {
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.MemorySize = 1024 * 1024;
            argon2.Iterations = 4;

            var newHashBytes = argon2.GetBytes(128);

            // Compare the new hash with the stored hash
            if (AreHashesEqual(hashBytes, newHashBytes))
            {
                return PasswordVerificationResult.Success;
            }
        }

        return PasswordVerificationResult.Failed;
    }

    private byte[] GenerateSalt()
    {
        var buffer = new byte[128 / 8];

        // Fill buffer with cryptographically secure random bytes
        RandomNumberGenerator.Fill(buffer); 

        return buffer;
    }

    private bool AreHashesEqual(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
        {
            return false;
        }

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }
}
