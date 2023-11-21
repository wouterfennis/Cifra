using Cifra.Domain;
using Cifra.Domain.Spreadsheet;
using System.IO;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Factory to create test results spreadsheets
    /// </summary>
    public interface ITestResultsSpreadsheetBuilder
    {
        /// <summary>
        /// Creates a test results spreadsheet
        /// </summary>
        Task<FileInfo> CreateTestResultsSpreadsheetAsync(Class @class, Test test, Metadata metadata);
    }
}