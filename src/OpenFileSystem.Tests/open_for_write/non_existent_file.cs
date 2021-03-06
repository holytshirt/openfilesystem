﻿using System.IO;
using NUnit.Framework;
using OpenFileSystem.IO;
using contexts;

namespace open_for_write
{
    [TestFixture(typeof(TestInMemoryFileSystem))]
    [TestFixture(typeof(TestLocalFileSystem))]
    public class non_existent_file<T> : files<T> where T : IFileSystem, new()
    {

        public non_existent_file()
        {
            given_temp_dir();
        }
        [TestCase(FileMode.Append)]
        [TestCase(FileMode.Create)]
        [TestCase(FileMode.CreateNew)]
        [TestCase(FileMode.OpenOrCreate)]
        public void file_is_created_for_mode(FileMode mode)
        {
            var tempFile = write_to_file(mode: mode);

            tempFile.Exists.ShouldBeTrue();
        }

        [TestCase(FileMode.Open)]
        [TestCase(FileMode.Truncate)]
        public void error_is_throw_for_mode(FileMode mode)
        {
            Executing(() => write_to_file(mode: mode)).ShouldThrow<FileNotFoundException>();
        }
    }
}
