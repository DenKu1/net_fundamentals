using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedCSharp.Lib.Tests
{
    public class TestFileSystemVisitor : FileSystemVisitor
    {
        public Func<string, IEnumerable<string>> enumerateDirectories;
        public Func<string, IEnumerable<string>> enumerateFiles;

        public TestFileSystemVisitor() : base() { }

        public TestFileSystemVisitor(Func<string, bool> filter) : base(filter) { }

        protected override IEnumerable<string> EnumerateDirectories(string path)
        {
            return enumerateDirectories?.Invoke(path) ?? Enumerable.Empty<string>();
        }

        protected override IEnumerable<string> EnumerateFiles(string path)
        {
            return enumerateFiles?.Invoke(path) ?? Enumerable.Empty<string>();
        }
    }
}
