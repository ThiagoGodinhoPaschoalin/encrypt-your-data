using EYD.Data.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EYD.Data.Models
{
    public class Decrypt : IDecrypt
    {
        public string Read(string pathFile)
        {
            try
            {
                return Utils.Read(pathFile);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ Decrypt ] [ READ: {pathFile} ]\n\t[ {ex.GetType().FullName} ] [ {ex.Message} ]\n");
                return null;
            }
        }

        public string Crypto(string content, string password, string salt)
        {
            try
            {
                byte[] cipherTextArray = Convert.FromBase64String(content);

                using (var aesAlg = new RijndaelManaged())
                {
                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt));
                    aesAlg.Key = deriveBytes.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = deriveBytes.GetBytes(aesAlg.IV.Length);

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextArray))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ Decrypt ] [ CRYPTO ]\n\t[ {ex.GetType().FullName} ] [ {ex.Message} ]\n");
                throw new InvalidOperationException("failed to try to decrypt", ex);
            }
        }

        public bool Save(string pathFile, string content)
        {
            try
            {
                var directory = Path.GetDirectoryName(pathFile);
                var fileName = Path.GetFileName(pathFile).Replace(Constants.Extension, "");
                string path = Path.Combine(directory, fileName);

                Utils.Save(path, content);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ Decrypt ] [ SAVE: {pathFile} ]\n\t[ {ex.GetType().FullName} ] [ {ex.Message} ]\n");
                return false;
            }
        }
    }
}