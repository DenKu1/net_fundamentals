using System;
using System.Linq;

using AdvancedCSharp.Lib;

namespace AdvancedCSharp.CLI
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter root path:");
            var path = Console.ReadLine();

            var fileSystemVisitor = new DirectoryFileSystemVisitor();
            var entryNames = fileSystemVisitor.EnumerateFileSystemEntriesRecursively(path.Trim()).ToList();

            entryNames.ForEach(entry => Console.WriteLine(entry));
        }
    }
}
