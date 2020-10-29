using Cifra.Application.Models.ValueTypes;
using System.IO;

namespace Cifra.FileSystem
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly Application.Models.ValueTypes.Path _classRepositoryPath;
        private readonly Application.Models.ValueTypes.Path _testRepositoryPath;
        private readonly Application.Models.ValueTypes.Path _spreadsheetDirectoryPath;

        public FileLocationProvider(Application.Models.ValueTypes.Path classRepositoryPath, Application.Models.ValueTypes.Path testRepositoryPath, Application.Models.ValueTypes.Path spreadsheetDirectoryPath)
        {
            _classRepositoryPath = classRepositoryPath;
            _testRepositoryPath = testRepositoryPath;
            _spreadsheetDirectoryPath = spreadsheetDirectoryPath;
        }

        public IFileInfoWrapper GetClassRepositoryPath()
        {
            return new FileInfoWrapper(_classRepositoryPath);
        }

        public IFileInfoWrapper GetTestRepositoryPath()
        {
            return new FileInfoWrapper(_testRepositoryPath);
        }

        public IDirectoryInfoWrapper GetSpreadsheetDirectoryPath()
        {
            return new DirectoryInfoWrapper(_spreadsheetDirectoryPath);
        }
    }
}
