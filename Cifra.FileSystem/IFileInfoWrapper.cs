using System.IO;

namespace Cifra.FileSystem
{
    public interface IFileInfoWrapper
    {
        Stream OpenRead();
        FileInfo ToFileInfo();
    }
}