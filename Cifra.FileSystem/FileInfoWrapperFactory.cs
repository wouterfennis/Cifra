using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem
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