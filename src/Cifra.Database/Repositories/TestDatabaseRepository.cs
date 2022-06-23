using Cifra.Core.Models.Validation;
using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database.Repositories
{
    /// <inheritdoc/>
    public class TestDatabaseRepository : ITestRepository
    {
        private readonly Context _dbContext;

        public TestDatabaseRepository(Context dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(Test newTest)
        {
            _ = newTest ?? throw new ArgumentNullException(nameof(newTest));

            _dbContext.Tests.Add(newTest);
            await _dbContext.SaveChangesAsync();

            return newTest.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Test>> GetAllAsync()
        {
            List<Test> entities = await _dbContext.Tests
                .Include(x => x.Assignments)
                .ToListAsync();
            return entities;
        }

        /// <inheritdoc/>
        public async Task<Test> GetAsync(int id)
        {
            Test findResult = await _dbContext.Tests
                .Include(x => x.Assignments)
                .SingleOrDefaultAsync(x => x.Id == id);
            return findResult;
        }

        /// <inheritdoc/>
        public async Task<ValidationMessage> UpdateAsync(Test updatedTest)
        {
            _dbContext.Tests.Update(updatedTest);
            await _dbContext.SaveChangesAsync();
            return null;
        }
    }
}
