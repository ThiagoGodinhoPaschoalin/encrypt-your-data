using System;

namespace EYD.Data.Flows
{
    public class DecryptOneFile
    {
        private readonly Interfaces.IDecrypt decrypt;

        public DecryptOneFile()
        {
            decrypt = new Models.Decrypt();
        }

        public bool Execute(string pathFile, string password, string salt, string destinationDirectory = null)
        {
            var readResult = decrypt.Read(pathFile);

            if (string.IsNullOrWhiteSpace(readResult))
            {
                Console.WriteLine($"[ DecryptOneFile ] [ Execute ] [ READ ]");
                return false;
            }

            var cryptoResult = decrypt.Crypto(readResult, password, salt);

            if (string.IsNullOrWhiteSpace(cryptoResult))
            {
                Console.WriteLine($"[ DecryptOneFile ] [ Execute ] [ CRYPTO ]");
                return false;
            }

            var saveWhere = Utils.GetFileDestinationPath(pathFile, destinationDirectory);

            var saveResult = decrypt.Save(saveWhere, cryptoResult);

            if (!saveResult)
            {
                Console.WriteLine($"[ DecryptOneFile ] [ Execute ] [ SAVE ]");
                return false;
            }

            return true;
        }
    }
}