using Cifra.Application.Models.ValueTypes;
using System.IO;

namespace Cifra.FileSystem
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly FilePath _filePath;

        public FileInfoWrapper(FilePath filePath)
        {
            _filePath = filePath;
        }

        public Stream OpenRead()
        {
            var fileInfo = new FileInfo(_filePath.Value);
            return fileInfo.OpenRead();
        }

        public FileInfo ToFileInfo()
        {
            return new FileInfo(_filePath.Value);
        }
    }
}
