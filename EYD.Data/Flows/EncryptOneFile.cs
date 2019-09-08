using System;
using System.Collections.Generic;
using System.Text;

namespace EYD.Data.Flows
{
    public class EncryptOneFile
    {
        private readonly Interfaces.IEncrypt encrypt;

        public EncryptOneFile()
        {
            encrypt = new Models.Encrypt();
        }

        public bool Execute(string pathFile, string password, string salt, string destinationDirectory = null )
        {
            var readResult = encrypt.Read(pathFile);

            if (string.IsNullOrWhiteSpace(readResult))
            {
                Console.WriteLine($"[ EncryptOneFile ] [ Execute ] [ READ ]");
                return false;
            }

            var cryptoResult = encrypt.Crypto(readResult, password, salt);

            if (string.IsNullOrWhiteSpace(cryptoResult))
            {
                Console.WriteLine($"[ EncryptOneFile ] [ Execute ] [ CRYPTO ]");
                return false;
            }

            var saveWhere = Utils.GetFileDestinationPath(pathFile, destinationDirectory);

            var saveResult = encrypt.Save(saveWhere, cryptoResult);

            if (!saveResult)
            {
                Console.WriteLine($"[ EncryptOneFile ] [ Execute ] [ SAVE ]");
                return false;
            }

            return true;
        }
    }
}