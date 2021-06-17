using System.IO;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <summary>
    /// Wrapper for <see cref="FileInfoWrapper"/>.
    /// </summary>
    public interface IFileInfoWrapper
    {
        /// <summary>
        /// Name of the file.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Fullname of the file.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Indicates if the file exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Open a read <see cref="Stream"/>.
        /// </summary>
        Stream OpenRead();

        /// <summary>
        /// Get <see cref="FileInfo"/>.
        /// </summary>
        FileInfo GetFileInfo();

        /// <summary>
        /// Open a write <see cref="Stream"/>.
        /// </summary>
        Stream OpenWrite();
    }
}