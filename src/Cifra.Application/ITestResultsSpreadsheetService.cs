using Cifra.Application.Models.Results;
using Cifra.Commands;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface ITestResultsSpreadsheetService
    {
        Task<CreateTestResultsSpreadsheetResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetCommand command);
    }
}