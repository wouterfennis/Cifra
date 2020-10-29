﻿using Cifra.Application.Models.ValueTypes;
using System.IO;

namespace Cifra.FileSystem
{
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly Application.Models.ValueTypes.Path _directoryPath;

        public DirectoryInfoWrapper(Application.Models.ValueTypes.Path directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public string FullName => _directoryPath.Value;

        public DirectoryInfo ToDirectoryInfo()
        {
            return new DirectoryInfo(_directoryPath.Value);
        }
    }
}