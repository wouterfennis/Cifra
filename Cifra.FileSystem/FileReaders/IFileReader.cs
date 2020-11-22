using Cifra.FileSystem.FileEntity;

namespace Cifra.FileSystem.FileReaders
{
    internal interface IFileReader
    {
        Class ReadClass(IFileInfoWrapper file);
    }
}