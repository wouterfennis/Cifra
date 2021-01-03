using Cifra.Application.Models.ValueTypes;
using System.IO;

namespace Cifra.FileSystem.FileSystemInfo
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly Application.Models.ValueTypes.Path _filePath;

        public string Name { get => GetFileInfo().Name; }

        public string FullName { get => GetFileInfo().FullName; }

        public bool Exists { get => GetFileInfo().Exists; }

        public FileInfoWrapper(Application.Models.ValueTypes.Path filePath)
        {
            _filePath = filePath;
        }

        public Stream OpenRead()
        {
            var fileInfo = GetFileInfo();
            return fileInfo.OpenRead();
        }

        public Stream OpenWrite()
        {
            var fileInfo = GetFileInfo();
            return fileInfo.OpenWrite();
        }

        public FileInfo GetFileInfo()
        {
            return new FileInfo(_filePath.Value);
        }
    }
}
