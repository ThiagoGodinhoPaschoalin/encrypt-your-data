using System;
using System.IO;
using System.Linq;

namespace EYD.ConsoleExecute
{
    public enum ActionType { Encrypt = 1, Decrypt = 2 }

    public static class Body
    {
        private static string FilePath { get; set; }
        private static ActionType Action { get; set; }
        private static string DestinationDirectory { get; set; }
        private static string Password { get; set; }
        private static string Salt { get; set; }



        public static void Get()
        {
            GetFilePath();
            GetDestinationDirectory();
            GetActionType();
            GetPassword();
            GetSalt();
        }

        public static void Set()
        {
            bool result = false;
            if(Action == ActionType.Encrypt)
            {
                result = (new Data.Flows.EncryptOneFile()).Execute(FilePath, Password, Salt, DestinationDirectory);
            }
            else if( Action == ActionType.Decrypt)
            {
                result = (new Data.Flows.DecryptOneFile()).Execute(FilePath, Password, Salt, DestinationDirectory);
            }
            else
            {
                Console.WriteLine("ALGO ERRADO no 'Body.Set()'.");
                Utils.CloseProgram(0);
            }

            if (result)
            {
                Console.WriteLine("SUCESSO");
            }
            else
            {
                Console.WriteLine("FALHA");
            }

        }

        private static void GetFilePath()
        {
            Console.WriteLine();
            Console.WriteLine("Escreva o caminho completo do seu arquivo, incluindo sua extensão.");
            Console.Write("Arquivo: ");
            var filePath = Utils.Keyboard(false);
            Console.WriteLine();

            if (!File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Arquivo '{filePath}' não encontrado.");
                Utils.CloseProgram(0);
            }

            FilePath = filePath;
        }
        private static void GetDestinationDirectory()
        {
            Console.WriteLine();
            Console.WriteLine("Escreva o caminho completo do diretório onde o resultado deve ser armazenado.");
            Console.WriteLine("Use 'VAZIO' para usar o mesmo diretório do arquivo!");
            Console.Write("Diretório: ");
            var directoryPath = Utils.Keyboard(false);
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                DestinationDirectory = Path.GetDirectoryName(FilePath);
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                var newDir = Directory.CreateDirectory(directoryPath);

                if (newDir.Exists)
                {
                    Console.WriteLine($"O diretório '{directoryPath}' não existe, mas foi criado!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"O diretório de destino não pôde ser criado.");
                    Utils.CloseProgram(0);
                }
            }

            DestinationDirectory = directoryPath;
        }
        private static void GetActionType()
        {
            Console.WriteLine();
            Console.WriteLine("[1] -> Encriptar; [2] -> Decriptar; Qualquer outra coisa, cancela!");
            Console.Write("Ação: ");
            var actionKey = Console.ReadKey();

            if (!(new[] { '1', '2' }).Any(d => d == actionKey.KeyChar))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ação inválida!");
                Utils.CloseProgram(0);
            }

            Action = actionKey.KeyChar == '1' ? ActionType.Encrypt : ActionType.Decrypt;
        }
        private static void GetPassword()
        {
            Console.WriteLine();
            Console.Write("Digite sua senha: ");
            var password = Utils.Keyboard(true);
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Senha não válida!");
                Utils.CloseProgram(0);
            }

            Password = Data.Utils.Hash(password);
        }
        private static void GetSalt()
        {
            Console.WriteLine();
            Console.Write("Digite sua senha de salto: ");
            var password = Utils.Keyboard(true);
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Salto não válido!");
                Utils.CloseProgram(0);
            }

            Salt = Data.Utils.Hash(password);
        }
    }
}