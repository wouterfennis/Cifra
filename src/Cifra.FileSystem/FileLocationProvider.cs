using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly Path _classRepositoryPath;
        private readonly Path _testRepositoryPath;
        private readonly Path _spreadsheetDirectoryPath;
        private readonly Path _magisterDirectoryPath;

        public FileLocationProvider(Path classRepositoryPath, 
            Path testRepositoryPath, 
            Path spreadsheetDirectoryPath,
            Path magisterDirectoryPath)
        {
            _classRepositoryPath = classRepositoryPath;
            _testRepositoryPath = testRepositoryPath;
            _spreadsheetDirectoryPath = spreadsheetDirectoryPath;
            _magisterDirectoryPath = magisterDirectoryPath;
        }

        public Path GetClassRepositoryPath()
        {
            return _classRepositoryPath;
        }

        public Path GetTestRepositoryPath()
        {
            return _testRepositoryPath;
        }

        public Path GetSpreadsheetDirectoryPath()
        {
            return _spreadsheetDirectoryPath;
        }

        public Path GetMagisterDirectoryPath()
        {
            return _magisterDirectoryPath;
        }
    }
}
