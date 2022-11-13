using Cifra.Domain.ValueTypes;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <summary>
    /// Factory to create IDirectoryInfoWrappers
    /// </summary>
    public interface IDirectoryInfoWrapperFactory
    {
        /// <summary>
        /// Creates a IDirectoryInfoWrapper from a Path
        /// </summary>
        IDirectoryInfoWrapper Create(Path path);
    }
}