using Cifra.Domain.ValueTypes;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage] // wrapper around filesystem.
    public class DirectoryInfoWrapperFactory : IDirectoryInfoWrapperFactory
    {
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DirectoryInfoWrapperFactory(IFileInfoWrapperFactory fileInfoWrapperFactory)
        {
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
        }

        /// <inheritdoc/>
        public IDirectoryInfoWrapper Create(Path path)
        {
            return new DirectoryInfoWrapper(path, _fileInfoWrapperFactory);
        }
    }
}