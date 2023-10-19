using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using System.Linq;
using Cifra.Domain.ValueTypes;

namespace Cifra.Database.Repositories
{
    /// <inheritdoc/>
    public class TestDatabaseRepository : ITestRepository
    {
        private readonly Context _dbContext;

        public TestDatabaseRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<uint> CreateAsync(Domain.Test newTest)
        {
            _ = newTest ?? throw new ArgumentNullException(nameof(newTest));

            _dbContext.Tests.Add(newTest);
            await _dbContext.SaveChangesAsync();

            return newTest.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Domain.Test>> GetAllAsync()
        {
            return await _dbContext.Tests
                 .AsNoTracking()
                 .Include(x => x.Assignments)
                 .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Domain.Test> GetAsync(uint id)
        {
            return await _dbContext.Tests
                .AsNoTracking()
                .Include(x => x.Assignments)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Name name)
        {
            var test = await _dbContext.Tests.SingleAsync(x => x.Name == name);
            _dbContext.Tests.Remove(test);

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<uint> UpdateAsync(Domain.Test updatedTest)
        {
            var updatedAssignmentsIds = updatedTest.Assignments.Select(x => x.Id).ToList();

            _dbContext.Tests.Update(updatedTest);
            _dbContext.Assignments.RemoveRange(_dbContext.Assignments.Where(x => !updatedAssignmentsIds.Contains(x.Id)));

            await _dbContext.SaveChangesAsync();

            return updatedTest.Id;
        }
    }
}
