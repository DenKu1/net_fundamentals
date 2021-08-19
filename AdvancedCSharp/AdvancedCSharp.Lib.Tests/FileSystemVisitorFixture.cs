using System;
using System.Collections.Generic;

using FluentAssertions;
using NUnit.Framework;

namespace AdvancedCSharp.Lib.Tests
{
    [TestFixture]
    public class FileSystemVisitorFixture
    {
        TestFileSystemVisitor sut;

        [SetUp]
        public void Setup()
        {
            sut = new TestFileSystemVisitor();
        }

        [Test]
        public void Should_return_expected_result_for_directory_with_no_subfolders()
        {
            List<string> expected = new() { "File1", "File2", "File3" };
            sut.enumerateFiles = (directory) => expected;

            var result = sut.EnumerateFileSystemEntriesRecursively("Folder0");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_return_expected_result_for_directory_with_subfolder()
        {
            List<string> expected = new() { "File1", "File2", "File3", "Folder1", "Folder1File1", "Folder1File2" };
            
            sut.enumerateFiles = (directory) => directory switch
            {
                "Folder0" => new List<string> { "File1", "File2", "File3" },
                "Folder1" => new List<string> { "Folder1File1", "Folder1File2" }
            };

            sut.enumerateDirectories = (directory) => directory switch
            {
                "Folder0" => new List<string> { "Folder1" },
                "Folder1" => new List<string> { }
            };

            var result = sut.EnumerateFileSystemEntriesRecursively("Folder0");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_stop_searching_at_second_file()
        {
            List<string> expected = new() { "File1" };
            
            sut.enumerateFiles = (directory) => new List<string> { "File1", "File2", "File3" };

            sut.FileFound += (s, e) => 
            {
                if (e.EntryName == "File2") 
                    e.BreakSearch = true; 
            };

            var result = sut.EnumerateFileSystemEntriesRecursively("Folder0");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_skip_second_file()
        {
            List<string> expected = new() { "File1", "File3" };

            sut.enumerateFiles = (directory) => new List<string> { "File1", "File2", "File3" };

            sut.FileFound += (s, e) =>
            {
                if (e.EntryName == "File2")
                    e.ExcludeEntry = true;
            };

            var result = sut.EnumerateFileSystemEntriesRecursively("Folder0");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Should_apply_filter_on_entries()
        {
            List<string> expected = new() { "File2" };
            Func<string, bool> filter = (entryName) => entryName.EndsWith("2");           
            
            var sut = new TestFileSystemVisitor(filter);
            sut.enumerateFiles = (directory) => new List<string> { "File1", "File2", "File3" };

            var result = sut.EnumerateFileSystemEntriesRecursively("Folder0");

            result.Should().BeEquivalentTo(expected);
        }
    }
}