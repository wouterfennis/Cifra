using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Cifra.FileSystem.FileSystemInfo
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage] // wrapper around filesystem.
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly Domain.ValueTypes.Path _directoryPath;
        private readonly IFileInfoWrapperFactory _fileInfoWrapperFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DirectoryInfoWrapper(Domain.ValueTypes.Path directoryPath, 
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
                .Select(x => _fileInfoWrapperFactory.Create(Domain.ValueTypes.Path.CreateFromString(x.FullName)))
                .ToArray();
        }
    }
}
