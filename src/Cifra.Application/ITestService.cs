using Cifra.Application.Models.Results;
using Cifra.Commands;
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
        Task<CreateTestResult> CreateTestAsync(CreateTestCommand command);

        /// <summary>
        /// Updates a test.
        /// </summary>
        Task<UpdateTestResult> UpdateTestAsync(UpdateTestCommand command);

        /// <summary>
        /// Retrieves all tests currently available.
        /// </summary>
        Task<GetAllTestsResult> GetTestsAsync();

        /// <summary>
        /// Retrieves test.
        /// </summary>
        Task<GetTestResult> GetTestAsync(uint id);

        /// <summary>
        /// Deletes a test.
        /// </summary>
        Task<DeleteTestResult> DeleteTestAsync(DeleteTestCommand command);
    }
}