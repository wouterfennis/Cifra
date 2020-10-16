using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly FilePath _classRepositoryLocation;
        private readonly FilePath _testRepositoryLocation;

        public FileLocationProvider(FilePath classRepositoryLocation, FilePath testRepositoryLocation)
        {
            _classRepositoryLocation = classRepositoryLocation;
            _testRepositoryLocation = testRepositoryLocation;
        }

        public IFileInfoWrapper GetClassRepositoryLocation()
        {
            return new FileInfoWrapper(_classRepositoryLocation);
        }

        public IFileInfoWrapper GetTestRepositoryLocation()
        {
            return new FileInfoWrapper(_testRepositoryLocation);
        }
    }
}
