using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    public class FileInfoWrapperFactory : IFileInfoWrapperFactory
    {
        /// <inheritdoc/>
        public IFileInfoWrapper Create(Path path)
        {
            return new FileInfoWrapper(path);
        }
    }
}