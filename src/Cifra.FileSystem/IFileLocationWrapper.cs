using Cifra.Application.Models.ValueTypes;

namespace Cifra.FileSystem
{
    /// <summary>
    /// Provider for file locations needed for storage and input.
    /// </summary>
    public interface IFileLocationProvider
    {
        /// <summary>
        /// Path where the class repository file is located.
        /// </summary>
        Path GetClassRepositoryPath();

        /// <summary>
        /// Path where the test repository file is located.
        /// </summary>
        Path GetTestRepositoryPath();

        /// <summary>
        /// Path where spreadsheet files should be saved.
        /// </summary>
        Path GetSpreadsheetDirectoryPath();

        /// <summary>
        /// Path where magister files with raw information is stored
        /// </summary>
        Path GetMagisterDirectoryPath();
    }
}