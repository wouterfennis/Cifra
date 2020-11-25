using System.IO;

namespace Cifra.FileSystem
{
    public interface IFileInfoWrapper
    {
        string Name { get; }
        string FullName { get; }
        Stream OpenRead();
        FileInfo ToFileInfo();
    }
}