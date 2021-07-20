using System;

using Task2;

namespace Task1a
{
    class Program
    {
        static void Main()
        {
            var username = Console.ReadLine();

            var message = MessageGenerator.CreateHelloMessage(username);

            Console.WriteLine(message);
        }
    }
}
