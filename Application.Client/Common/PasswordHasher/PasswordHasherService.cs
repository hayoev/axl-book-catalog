using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Application.Client.Common.PasswordHasher
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private const int SaltSize = 16;
        private const int NumBytesRequested = 32;
        private const int IterationCount = 1000;

        public (string passwordHash, string passwordSalt) Create(string password, string globalSalt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be empty or null .", nameof(password));

            if (string.IsNullOrEmpty(globalSalt))
                throw new ArgumentException("Value cannot be empty or null .", nameof(globalSalt));

            var salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var passwordSalt = Convert.ToBase64String(salt);

            var passwordHash = Convert
                .ToBase64String(KeyDerivation.Pbkdf2(string.Concat(password, globalSalt), salt,
                    KeyDerivationPrf.HMACSHA256, IterationCount, NumBytesRequested));

            return (passwordHash, passwordSalt);
        }

        public bool Verify(string enteredPassword, string storedPassword, string storedSalt, string globalSalt)
        {
            if (string.IsNullOrEmpty(enteredPassword))
                throw new ArgumentException("Value cannot be empty or null .", nameof(enteredPassword));

            if (string.IsNullOrEmpty(storedPassword))
                throw new ArgumentException("Value cannot be empty or null .", nameof(storedPassword));

            if (string.IsNullOrEmpty(storedSalt))
                throw new ArgumentException("Value cannot be empty or null .", nameof(storedSalt));

            if (string.IsNullOrEmpty(globalSalt))
                throw new ArgumentException("Value cannot be empty or null .", nameof(globalSalt));

            var saltBytes = Convert.FromBase64String(storedSalt);

            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(string.Concat(enteredPassword, globalSalt),
                saltBytes, KeyDerivationPrf.HMACSHA256, IterationCount, NumBytesRequested));

            return passwordHash == storedPassword;
        }
    }
}