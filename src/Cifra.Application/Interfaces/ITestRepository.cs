using Cifra.Application.Models.Test;
using Cifra.Application.Models.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Repository for Tests
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// Retrieves a test
        /// </summary>
        Task<Test> GetAsync(int id);

        /// <summary>
        /// Create a test 
        /// </summary>
        Task CreateAsync(Test newTest);

        /// <summary>
        /// Updates a test
        /// </summary>
        Task<ValidationMessage> UpdateAsync(Test test);

        /// <summary>
        /// Get all tests
        /// </summary>
        Task<IEnumerable<Test>> GetAllAsync();
    }
}
