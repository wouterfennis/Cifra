using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using System.Linq;
using Cifra.Database.Mapping;

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
        public async Task<int> CreateAsync(Domain.Test newTest)
        {
            _ = newTest ?? throw new ArgumentNullException(nameof(newTest));

            var entity = newTest.MapToSchema();

            _dbContext.Tests.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Domain.Test>> GetAllAsync()
        {
            List<Test> entities = await _dbContext.Tests
                .AsNoTracking()
                .Include(x => x.Assignments)
                .ToListAsync();
            return entities.MapToDomain();
        }

        /// <inheritdoc/>
        public async Task<Domain.Test> GetAsync(int id)
        {
            Test findResult = await _dbContext.Tests
                .AsNoTracking()
                .Include(x => x.Assignments)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return findResult.MapToDomain();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(Domain.Test updatedTest)
        {
            var updatedEntity = updatedTest.MapToSchema();
            var updatedAssignmentsIds = updatedEntity.Assignments.Select(x => x.Id).ToList();

            _dbContext.Tests.Update(updatedEntity);
            _dbContext.Assignments.RemoveRange(_dbContext.Assignments.Where(x => !updatedAssignmentsIds.Contains(x.Id)));

            await _dbContext.SaveChangesAsync();

            return updatedEntity.Id;
        }
    }
}
