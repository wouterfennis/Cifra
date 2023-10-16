using Cifra.Application.Models.Commands;
using Cifra.Application.Models.Results;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface ITestResultsSpreadsheetService
    {
        Task<CreateTestResultsSpreadsheetResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetCommand command);
    }
}