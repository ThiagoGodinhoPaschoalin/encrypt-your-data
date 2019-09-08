using System;
using System.Text;

namespace EYD.ConsoleExecute
{
    public static class Utils
    {
        public static string Keyboard(bool secretContent)
        {
            StringBuilder text = new StringBuilder();
            do
            {
                var userKey = Console.ReadKey(secretContent);

                if (userKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    text.Append(userKey.KeyChar);
                    if (secretContent)
                    {
                        Console.Write("*");
                    }
                }
            }
            while (true);

            return text.ToString();
        }

        public static void CloseProgram(int exitCode)
        {
            Console.WriteLine("O programa foi encerrado!");
            Environment.Exit(exitCode);
        }
    }
}