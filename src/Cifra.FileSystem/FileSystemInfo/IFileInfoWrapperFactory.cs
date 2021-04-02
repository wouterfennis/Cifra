using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <summary>
    /// Factory to create IFileInfoWrappers
    /// </summary>
    public interface IFileInfoWrapperFactory
    {
        /// <summary>
        /// Creates a IFileInfoWrapper from a Path
        /// </summary>
        IFileInfoWrapper Create(Path path);
    }
}