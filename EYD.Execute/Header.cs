using System;

namespace EYD.ConsoleExecute
{
    public static class Header
    {
        public static void Get()
        {
            Console.WriteLine();
            Console.WriteLine(new string('#', Console.WindowWidth));

            string title = "Criptografe seus arquivos";
            int titleBorder = ((Console.WindowWidth - title.Length) / 2);
            Console.WriteLine($"{new string(' ', titleBorder)}{title}{new string(' ', titleBorder)}");

            Console.WriteLine(new string('#', Console.WindowWidth));
            Console.WriteLine();

            Console.WriteLine("version 0.1.0");

            Console.WriteLine(new string('#', Console.WindowWidth));
            Console.WriteLine();
        }
    }
}