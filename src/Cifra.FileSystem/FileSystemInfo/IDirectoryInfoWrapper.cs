using System.IO;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <summary>
    /// Wrapper around the <see cref="DirectoryInfo"/>.
    /// </summary>
    public interface IDirectoryInfoWrapper
    {
        /// <summary>
        /// The fullname of the directory.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Converts the wrapper to <see cref="DirectoryInfo"/>
        /// </summary>
        /// <returns></returns>
        DirectoryInfo ToDirectoryInfo();

        /// <summary>
        /// Gets a list with files inside the directory.
        /// </summary>
        /// <returns></returns>
        IFileInfoWrapper[] GetFiles();
    }
}