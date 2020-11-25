using Cifra.Application.Models.ValueTypes;
using System.IO;

namespace Cifra.FileSystem
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly Application.Models.ValueTypes.Path _filePath;

        public string Name { get => ToFileInfo().Name; }

        public string FullName { get => ToFileInfo().FullName; }

        public FileInfoWrapper(Application.Models.ValueTypes.Path filePath)
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
