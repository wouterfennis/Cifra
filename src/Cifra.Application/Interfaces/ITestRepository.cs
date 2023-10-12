﻿using Cifra.Domain;
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
        Task<int> CreateAsync(Test newTest);

        /// <summary>
        /// Updates a test
        /// </summary>
        Task<int> UpdateAsync(Test test);

        /// <summary>
        /// Get all tests
        /// </summary>
        Task<List<Test>> GetAllAsync();

        /// <summary>
        /// Get all tests
        /// </summary>
        Task DeleteAsync(int id);
    }
}
