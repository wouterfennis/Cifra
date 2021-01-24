using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    public interface ITestService
    {
        Task<AddAssignmentResult> AddAssignmentAsync(AddAssignmentRequest model);
        Task<AddQuestionResult> AddQuestionAsync(AddQuestionRequest model);
        Task<CreateTestResult> CreateTestAsync(CreateTestRequest model);
        Task<GetAllTestsResult> GetTestsAsync();
    }
}