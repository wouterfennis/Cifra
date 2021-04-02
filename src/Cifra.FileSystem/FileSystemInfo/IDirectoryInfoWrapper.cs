using System.IO;

namespace Cifra.FileSystem.FileSystemInfo
{
    public interface IDirectoryInfoWrapper
    {
        string FullName { get; }

        DirectoryInfo ToDirectoryInfo();
        IFileInfoWrapper[] GetFiles();
    }
}