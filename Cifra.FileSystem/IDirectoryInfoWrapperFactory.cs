using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem
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