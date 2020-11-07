using Cifra.Application.Models.Class;
using Cifra.Application.Models.Spreadsheet;
using Cifra.Application.Models.Test;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Factory to create test results spreadsheets
    /// </summary>
    public interface ITestResultsSpreadsheetFactory
    {
        /// <summary>
        /// Creates a test results spreadsheet
        /// </summary>
        Task CreateTestResultsSpreadsheetAsync(Class @class, Test test, Metadata metadata);
    }
}