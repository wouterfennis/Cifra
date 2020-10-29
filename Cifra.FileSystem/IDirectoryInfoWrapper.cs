using System.IO;

namespace Cifra.FileSystem
{
    public interface IDirectoryInfoWrapper
    {
        string FullName { get; }

        DirectoryInfo ToDirectoryInfo();
    }
}