using System.IO;
using System.Linq;

namespace Cifra.FileSystem
{
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly Application.Models.ValueTypes.Path _directoryPath;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        public DirectoryInfoWrapper(Application.Models.ValueTypes.Path directoryPath, 
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
                .Select(x => _fileInfoWrapperFactory.Create(Application.Models.ValueTypes.Path.CreateFromString(x.FullName)))
                .ToArray();
        }
    }
}
