using System.IO;

namespace Cifra.FileSystem.FileSystemInfo
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly Core.Models.ValueTypes.Path _filePath;

        public string Name { get => GetFileInfo().Name; }

        public string FullName { get => GetFileInfo().FullName; }

        public bool Exists { get => GetFileInfo().Exists; }

        public FileInfoWrapper(Core.Models.ValueTypes.Path filePath)
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
