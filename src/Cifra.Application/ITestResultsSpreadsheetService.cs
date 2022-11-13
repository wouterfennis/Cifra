using Cifra.Application.Models.Spreadsheet.Commands;
using Cifra.Application.Models.Spreadsheet.Results;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface ITestResultsSpreadsheetService
    {
        Task<CreateTestResultsSpreadsheetResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetCommand command);
    }
}