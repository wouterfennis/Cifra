using Cifra.Application.Models.Class;
using Cifra.Application.Models.Spreadsheet;
using Cifra.Application.Models.Test;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    public interface ITestResultsSpreadsheetFactory
    {
        Task CreateTestResultsSpreadsheetAsync(Class @class, Test test, Metadata metadata);
    }
}