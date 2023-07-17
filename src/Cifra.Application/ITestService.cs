using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using System.Threading.Tasks;

namespace Cifra.Application
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
        /// Updates a test.
        /// </summary>
        Task<UpdateTestResult> UpdateTestAsync(UpdateTestCommand model);

        /// <summary>
        /// Retrieves all tests currently available.
        /// </summary>
        Task<GetAllTestsResult> GetTestsAsync();

        /// <summary>
        /// Retrieves test.
        /// </summary>
        Task<GetTestResult> GetTestAsync(int id);
    }
}