﻿// The MIT License (MIT)

// Copyright (c) 2014 Ben Abelshausen

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections.Generic;
using System.IO;

namespace GTFS.IO
{
    /// <summary>
    /// Represents a list of GTFS source files all located in a given directory.
    /// </summary>
    public class GTFSDirectorySource : IEnumerable<IGTFSSourceFile>
    {
        /// <summary>
        /// Holds the directory.
        /// </summary>
        private DirectoryInfo _directory;

        /// <summary>
        /// Creates a new GTFS directory source.
        /// </summary>
        /// <param name="path"></param>
        public GTFSDirectorySource(string path)
            : this(new DirectoryInfo(path))
        {

        }

        /// <summary>
        /// Creates a new GTFS directory source.
        /// </summary>
        /// <param name="directory"></param>
        public GTFSDirectorySource(DirectoryInfo directory)
        {
            _directory = directory;
        }

        /// <summary>
        /// Builds a list of source files;
        /// </summary>
        /// <returns></returns>
        private List<IGTFSSourceFile> BuildSource()
        {
            FileInfo[] files = _directory.GetFiles("*.txt");
            List<IGTFSSourceFile> sourceFiles = new List<IGTFSSourceFile>(files.Length);
            foreach(var file in files)
            {
                sourceFiles.Add(new GTFSSourceFileStream(file.OpenRead(), file.Name));
            }
            return sourceFiles;
        }

        /// <summary>
        /// Returns the enumerator for this IEnumerable.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IGTFSSourceFile> GetEnumerator()
        {
            return this.BuildSource().GetEnumerator();
        }

        /// <summary>
        /// Returns the enumerator for this IEnumerable.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.BuildSource().GetEnumerator();
        }
    }
}