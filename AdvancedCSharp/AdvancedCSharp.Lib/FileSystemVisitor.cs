using System;
using System.Collections.Generic;

namespace AdvancedCSharp.Lib
{
    public class EntryFoundArgs : EventArgs
    {
        public bool ExcludeEntry { get; set; }
        public bool BreakSearch { get; set; }

        public string EntryName { get; }      

        public EntryFoundArgs(string entryName)
        {
            EntryName = entryName;
        }
    }

    public abstract class FileSystemVisitor
    {
        readonly Func<string, bool> filter;

        public event Action EnumerationStarted;
        public event Action EnumerationFinished;

        public event EventHandler<EntryFoundArgs> FileFound;
        public event EventHandler<EntryFoundArgs> DirectoryFound;
        public event EventHandler<EntryFoundArgs> FilteredFileFound;
        public event EventHandler<EntryFoundArgs> FilteredDirectoryFound;
        
        public FileSystemVisitor() { }

        public FileSystemVisitor(Func<string, bool> filter)
        {
            this.filter = filter;
        }

        public IEnumerable<string> EnumerateFileSystemEntriesRecursively(string path)
        {
            EnumerationStarted?.Invoke();

            foreach (var subDirectory in EnumerateFilteredDirectories(path))
            {
                yield return subDirectory;
            }

            foreach (var file in EnumerateFilteredFiles(path))
            {
                yield return file;
            }

            EnumerationFinished?.Invoke();
        }

        IEnumerable<string> EnumerateFilteredDirectories(string parentDirectory)
        {
            var directories = EnumerateDirectories(parentDirectory);

            foreach (var directory in directories)
            {
                var args = new EntryFoundArgs(directory);
                DirectoryFound?.Invoke(this, args);

                if (SatisfiesFilteringCondition(directory))
                {
                    FilteredDirectoryFound?.Invoke(this, args);

                    if (args.BreakSearch)
                    {
                        yield break;
                    }

                    if (!args.ExcludeEntry)
                    {
                        yield return directory;
                    }
                }

                foreach (var subDirectory in EnumerateFileSystemEntriesRecursively(directory))
                {
                    yield return subDirectory;
                }
            }
        }

        IEnumerable<string> EnumerateFilteredFiles(string directory)
        {
            var files = EnumerateFiles(directory);

            foreach (var file in files)
            {
                var args = new EntryFoundArgs(file);
                FileFound?.Invoke(this, args);

                if (SatisfiesFilteringCondition(file))
                {
                    FilteredFileFound?.Invoke(this, args);

                    if (args.BreakSearch)
                    {
                        yield break;
                    }

                    if (!args.ExcludeEntry)
                    {
                        yield return file;
                    }
                }
            }
        }

        bool SatisfiesFilteringCondition(string fileSystemEntry)
        {
            return filter == null || filter(fileSystemEntry);
        }

        protected abstract IEnumerable<string> EnumerateDirectories(string path);
        protected abstract IEnumerable<string> EnumerateFiles(string path);
    }
}
