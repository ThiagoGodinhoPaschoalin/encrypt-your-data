using System;
using System.IO;

namespace EYD.Data.Flows
{
    public class FirstTestFlow
    {
        public void Test(string pathFile = @"C:\Users\thiago.user\Desktop\teste\arquivo.pdf", string password = "123456789", string salt = "123456789")
        {
            string directory = Path.GetDirectoryName(pathFile);
            string fileName = Path.GetFileName(pathFile);

            string content = string.Empty;
            using (FileStream fs = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    content = Convert.ToBase64String(ms.ToArray());
                }
            }

            ///Encrypt
            EYD.Data.Models.Encrypt encrypt = new Data.Models.Encrypt();
            var dataEncrypted = encrypt.Crypto(content, password, salt);

            string EncryptPath = Path.Combine(directory, string.Concat("encrypt_", fileName));
            var EncryptBytes = Convert.FromBase64String(dataEncrypted);
            using (FileStream fs = new FileStream(EncryptPath, FileMode.CreateNew, FileAccess.Write))
            {
                fs.Write(EncryptBytes, 0, EncryptBytes.Length);
            }

            ///Decrypt
            using (FileStream fs = new FileStream(EncryptPath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    content = Convert.ToBase64String(ms.ToArray());
                }
            }

            EYD.Data.Models.Decrypt decrypt = new Data.Models.Decrypt();
            var dataDecrypted = decrypt.Crypto(content, password, salt);

            string DecryptPath = Path.Combine(directory, string.Concat("decrypt_", fileName));
            var DecryptBytes = Convert.FromBase64String(dataDecrypted);

            using (FileStream fs = new FileStream(DecryptPath, FileMode.CreateNew, FileAccess.Write))
            {
                fs.Write(DecryptBytes, 0, DecryptBytes.Length);
            }
        }
    }
}