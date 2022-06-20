using Cifra.Core.Models.ValueTypes;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage] // wrapper around filesystem.
    public class FileInfoWrapperFactory : IFileInfoWrapperFactory
    {
        /// <inheritdoc/>
        public IFileInfoWrapper Create(Path path)
        {
            return new FileInfoWrapper(path);
        }
    }
}