using System.IO;
using System.Linq;

namespace Cifra.FileSystem.FileSystemInfo
{
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly Core.Models.ValueTypes.Path _directoryPath;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        public DirectoryInfoWrapper(Core.Models.ValueTypes.Path directoryPath, 
            IFileInfoWrapperFactory fileInfoWrapperFactory)
        {
            _directoryPath = directoryPath;
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
        }

        public string FullName => _directoryPath.Value;

        public DirectoryInfo ToDirectoryInfo()
        {
            return new DirectoryInfo(_directoryPath.Value);
        }

        public IFileInfoWrapper[] GetFiles()
        {
            return ToDirectoryInfo()
                .GetFiles()
                .Select(x => _fileInfoWrapperFactory.Create(Core.Models.ValueTypes.Path.CreateFromString(x.FullName)))
                .ToArray();
        }
    }
}
