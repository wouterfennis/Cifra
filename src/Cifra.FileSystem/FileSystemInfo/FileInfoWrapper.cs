using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage] // wrapper around filesystem.
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly Domain.ValueTypes.Path _filePath;

        /// <inheritdoc/>
        public string Name { get => GetFileInfo().Name; }

        /// <inheritdoc/>
        public string FullName { get => GetFileInfo().FullName; }

        /// <inheritdoc/>
        public bool Exists { get => GetFileInfo().Exists; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FileInfoWrapper(Domain.ValueTypes.Path filePath)
        {
            _filePath = filePath;
        }

        /// <inheritdoc/>
        public Stream OpenRead()
        {
            var fileInfo = GetFileInfo();
            return fileInfo.OpenRead();
        }

        /// <inheritdoc/>
        public Stream OpenWrite()
        {
            var fileInfo = GetFileInfo();
            return fileInfo.OpenWrite();
        }

        /// <inheritdoc/>
        public FileInfo GetFileInfo()
        {
            return new FileInfo(_filePath.Value);
        }
    }
}
