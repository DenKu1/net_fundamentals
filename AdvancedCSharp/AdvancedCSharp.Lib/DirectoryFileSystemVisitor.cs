using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedCSharp.Lib
{
    public class DirectoryFileSystemVisitor : FileSystemVisitor
    {
        public DirectoryFileSystemVisitor() : base() { }

        public DirectoryFileSystemVisitor(Func<string, bool> filter) : base(filter) { }

        protected override IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        protected override IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }
    }
}
