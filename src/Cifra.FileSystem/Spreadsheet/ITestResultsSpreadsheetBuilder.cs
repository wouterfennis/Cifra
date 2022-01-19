using Cifra.Core.Models.Class;
using Cifra.Core.Models.Spreadsheet;
using Cifra.Core.Models.Test;
using System.Threading.Tasks;

namespace Cifra.FileSystem.Spreadsheet
{
    /// <summary>
    /// Factory to create test results spreadsheets
    /// </summary>
    public interface ITestResultsSpreadsheetBuilder
    {
        /// <summary>
        /// Creates a test results spreadsheet
        /// </summary>
        Task<SaveResult> CreateTestResultsSpreadsheetAsync(Class @class, Test test, Metadata metadata);
    }
}