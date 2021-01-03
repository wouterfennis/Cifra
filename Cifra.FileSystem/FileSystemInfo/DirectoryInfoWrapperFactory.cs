using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    public class DirectoryInfoWrapperFactory : IDirectoryInfoWrapperFactory
    {
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

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