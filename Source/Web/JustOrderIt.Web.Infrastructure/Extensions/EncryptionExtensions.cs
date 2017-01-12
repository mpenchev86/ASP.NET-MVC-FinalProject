namespace JustOrderIt.Web.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public static class EncryptionExtensions
    {
        private const string Salt = "jU$T0rd3r1t";

        public static string ToMd5Hash(this int input)
        {
            return input.ToString().ToMd5Hash();
        }

        public static string ToMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}{1}", input, Salt)));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            foreach (byte symbol in data)
            {
                builder.Append(symbol.ToString("x2"));
            }

            // Return the hexadecimal string.
            return builder.ToString();
        }
    }
}
