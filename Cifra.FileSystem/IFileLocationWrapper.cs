namespace Cifra.FileSystem
{
    public interface IFileLocationProvider
    {
        IFileInfoWrapper GetClassRepositoryLocation();

        IFileInfoWrapper GetTestRepositoryLocation();
    }
}