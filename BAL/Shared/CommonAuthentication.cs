using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BAL.Shared
{
    public static class CommonAuthentication
    {
        public static byte[] CreatePasswordHash(string password)
        {
            using (var sha512 = SHA256.Create())
            {
                return sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //public static bool VerifyPasswordHash(string password, byte[] storedHash)
        //{
        //    using (var sha512 = SHA256.Create())
        //    {
        //        var computedHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(storedHash);
        //    }
        //}

        public static bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var computedHashBase64 = Convert.ToBase64String(computedHash);
                return computedHashBase64 == storedHash;
            }
        }

    }
}
