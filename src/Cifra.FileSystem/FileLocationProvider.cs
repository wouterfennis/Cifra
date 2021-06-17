using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem
{
    /// <inheritdoc/>
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly Path _classRepositoryPath;
        private readonly Path _testRepositoryPath;
        private readonly Path _spreadsheetDirectoryPath;
        private readonly Path _classesDirectoryPath;

        public FileLocationProvider(Path classRepositoryPath, 
            Path testRepositoryPath, 
            Path spreadsheetDirectoryPath,
            Path classesDirectoryPath)
        {
            _classRepositoryPath = classRepositoryPath;
            _testRepositoryPath = testRepositoryPath;
            _spreadsheetDirectoryPath = spreadsheetDirectoryPath;
            _classesDirectoryPath = classesDirectoryPath;
        }

        /// <inheritdoc/>
        public Path GetClassRepositoryPath()
        {
            return _classRepositoryPath;
        }

        /// <inheritdoc/>
        public Path GetTestRepositoryPath()
        {
            return _testRepositoryPath;
        }

        /// <inheritdoc/>
        public Path GetSpreadsheetDirectoryPath()
        {
            return _spreadsheetDirectoryPath;
        }

        /// <inheritdoc/>
        public Path GetClassesDirectoryPath()
        {
            return _classesDirectoryPath;
        }
    }
}
