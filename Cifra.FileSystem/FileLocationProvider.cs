using Cifra.Application.Models.ValueTypes;
using System.IO;

namespace Cifra.FileSystem
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly Application.Models.ValueTypes.Path _classRepositoryPath;
        private readonly Application.Models.ValueTypes.Path _testRepositoryPath;
        private readonly Application.Models.ValueTypes.Path _spreadsheetDirectoryPath;
        private readonly Application.Models.ValueTypes.Path _magisterDirectoryPath;

        public FileLocationProvider(Application.Models.ValueTypes.Path classRepositoryPath, 
            Application.Models.ValueTypes.Path testRepositoryPath, 
            Application.Models.ValueTypes.Path spreadsheetDirectoryPath,
            Application.Models.ValueTypes.Path magisterDirectoryPath)
        {
            _classRepositoryPath = classRepositoryPath;
            _testRepositoryPath = testRepositoryPath;
            _spreadsheetDirectoryPath = spreadsheetDirectoryPath;
            _magisterDirectoryPath = magisterDirectoryPath;
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

        public IDirectoryInfoWrapper GetMagisterDirectoryPath()
        {
            return new DirectoryInfoWrapper(_magisterDirectoryPath);
        }
    }
}
