using Cifra.Core.Models.Spreadsheet;
using System.Threading.Tasks;

namespace Cifra.Application
{
    internal interface ITestResultsSpreadsheetService
    {
        Task<SaveResult> CreateTestResultsSpreadsheetAsync(int classId, int testId, Metadata metadata);
    }
}