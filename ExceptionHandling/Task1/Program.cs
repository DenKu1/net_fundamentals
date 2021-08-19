using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line.Length > 0)
                {
                    Console.WriteLine(line[0]);
                }
            }
        }
    }
}