using System.Threading.Tasks;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Application Service for the Test entity.
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// Adds an assignment to a test.
        /// </summary>
        Task<AddAssignmentResult> AddAssignmentAsync(AddAssignmentRequest model);

        /// <summary>
        /// Creates a test.
        /// </summary>
        Task<CreateTestResult> CreateTestAsync(CreateTestRequest model);

        /// <summary>
        /// Retrieves all tests currently available.
        /// </summary>
        Task<GetAllTestsResult> GetTestsAsync();
    }
}