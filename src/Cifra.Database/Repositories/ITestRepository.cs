﻿using Cifra.Core.Models.Validation;
using Cifra.Database.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database.Repositories
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
        Task<ValidationMessage> UpdateAsync(Test test);

        /// <summary>
        /// Get all tests
        /// </summary>
        Task<List<Test>> GetAllAsync();
    }
}