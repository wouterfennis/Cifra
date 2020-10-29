namespace Cifra.FileSystem
{
    public interface IFileLocationProvider
    {
        IFileInfoWrapper GetClassRepositoryPath();

        IFileInfoWrapper GetTestRepositoryPath();

        IFileInfoWrapper GetSpreadsheetDirectoryPath();
    }
}