using System.IO;

namespace Cifra.FileSystem
{
    public interface IFileInfoWrapper
    {
        string Name { get; }
        string FullName { get; }
        bool Exists { get; }

        Stream OpenRead();
        FileInfo GetFileInfo();
        Stream OpenWrite();
    }
}