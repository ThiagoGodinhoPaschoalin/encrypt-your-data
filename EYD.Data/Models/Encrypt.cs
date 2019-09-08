using EYD.Data.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EYD.Data.Models
{
    public class Encrypt : IEncrypt
    {
        public string Read(string pathFile)
        {
            try
            {
                var byteFile = File.ReadAllBytes(pathFile);
                return Convert.ToBase64String(byteFile);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ Encrypt ] [ READ: {pathFile} ]\n\t[ {ex.GetType().FullName} ] [ {ex.Message} ]\n");
                return null;
            }
        }

        public string Crypto(string content, string password, string salt)
        {
            try
            {
                using (var aesAlg = new RijndaelManaged())
                {
                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt));
                    aesAlg.Key = deriveBytes.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = deriveBytes.GetBytes(aesAlg.IV.Length);

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(content);
                            }
                            return Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ Encrypt ] [ CRYPTO ]\n\t[ {ex.GetType().FullName} ] [ {ex.Message} ]\n");
                throw new InvalidOperationException("failed to try to encrypt!", ex);
            }
        }

        public bool Save(string pathFile, string content)
        {
            try
            {
                var directory = Path.GetDirectoryName(pathFile);
                var fileName = Path.GetFileName(pathFile);

                string path = Path.Combine(directory, string.Concat(fileName, Constants.Extension));
                File.WriteAllText(path, content);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ Encrypt ] [ SAVE: {pathFile} ]\n\t[ {ex.GetType().FullName} ] [ {ex.Message} ]\n");
                return false;
            }
        }
    }
}