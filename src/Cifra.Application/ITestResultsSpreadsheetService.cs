using Cifra.Application.Models.Spreadsheet.Commands;
using Cifra.Application.Models.Spreadsheet.Results;
using Cifra.Core.Models.Spreadsheet;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface ITestResultsSpreadsheetService
    {
        Task<CreateTestResultsSpreadsheetResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetCommand command);
    }
}