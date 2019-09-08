using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EYD.Data
{
    public static class Utils
    {
        public static string GetFileDestinationPath(string fileOriginPath, string diretoryDestinationPath)
        {
            if (string.IsNullOrWhiteSpace(fileOriginPath))
            {
                throw new InvalidOperationException("You need pass the origin path!");
            }

            if (string.IsNullOrWhiteSpace(diretoryDestinationPath))
            {
                return fileOriginPath;
            }
            else
            {
                var fileName = Path.GetFileName(fileOriginPath);
                return Path.Combine(diretoryDestinationPath, fileName);
            }
        }

        public static string Hash(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new InvalidOperationException("Hash failed because your parameter is null!");
            }

            try
            {
                using (SHA512Managed sha = new SHA512Managed())
                {
                    var hashByte = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                    return string.Join(string.Empty, hashByte.Select(b => b.ToString("X2")));
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Exception in 'Hash' with text '{text}'.", ex);
            }
        }
    }
}