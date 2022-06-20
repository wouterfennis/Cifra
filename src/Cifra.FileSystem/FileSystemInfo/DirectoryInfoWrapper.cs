using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage] // wrapper around filesystem.
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly Core.Models.ValueTypes.Path _directoryPath;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DirectoryInfoWrapper(Core.Models.ValueTypes.Path directoryPath, 
            IFileInfoWrapperFactory fileInfoWrapperFactory)
        {
            _directoryPath = directoryPath;
            _fileInfoWrapperFactory = fileInfoWrapperFactory;
        }

        public string FullName => _directoryPath.Value;

        /// <inheritdoc/>
        public DirectoryInfo ToDirectoryInfo()
        {
            return new DirectoryInfo(_directoryPath.Value);
        }

        /// <inheritdoc/>
        public IFileInfoWrapper[] GetFiles()
        {
            return ToDirectoryInfo()
                .GetFiles()
                .Select(x => _fileInfoWrapperFactory.Create(Core.Models.ValueTypes.Path.CreateFromString(x.FullName)))
                .ToArray();
        }
    }
}
