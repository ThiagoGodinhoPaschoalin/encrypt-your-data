using System;

namespace EYD.ConsoleExecute
{
    class Program
    {
        protected Program() { }

        static void Main(string[] args)
        {
            while (true)
            {
                Header.Get();
                Body.Get();
                Body.Set();

                Console.WriteLine("\n\nCONTINUE? [Y] => YES;");
                var theKey = Console.ReadKey();
                if(theKey.Key != ConsoleKey.Y)
                {
                    break;
                }

                Console.Clear();
            }
        }
    }
}