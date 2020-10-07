using Cifra.Application.Models.Test;
using Cifra.Application.Validation;
using System;

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
        Test Get(Guid id);

        /// <summary>
        /// Create a test 
        /// </summary>
        ValidationMessage Create(Test test);

        /// <summary>
        /// Updates a test
        /// </summary>
        ValidationMessage Update(Test test);
    }
}
