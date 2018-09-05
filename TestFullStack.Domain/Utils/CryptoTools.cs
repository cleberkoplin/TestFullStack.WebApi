using System;
using System.Text;
using System.Security.Cryptography;

namespace TestFullStack.Domain.Utils
{
    public class CryptoTools
    {
        public static string ComputeHashMd5(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var algorithm = new MD5CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
