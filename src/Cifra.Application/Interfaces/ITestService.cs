using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Application Service for the Test entity.
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// Creates a test.
        /// </summary>
        Task<CreateTestResult> CreateTestAsync(CreateTestCommand model);

        /// <summary>
        /// Adds an assignment to a test.
        /// </summary>
        Task<AddAssignmentResult> AddAssignmentAsync(AddAssignmentCommand model);

        /// <summary>
        /// Retrieves all tests currently available.
        /// </summary>
        Task<GetAllTestsResult> GetTestsAsync();
    }
}