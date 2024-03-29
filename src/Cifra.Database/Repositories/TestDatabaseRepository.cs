﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;

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
        public async Task<Domain.Test?> GetAsync(uint id)
        {
            return await _dbContext.Tests
                .Include(x => x.Assignments)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(uint id)
        {
            var test = await _dbContext.Tests.SingleAsync(x => x.Id == id);
            _dbContext.Tests.Remove(test);

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<uint> UpdateAsync(Domain.Test updatedTest)
        {
            _dbContext.Tests.Update(updatedTest);

            await _dbContext.SaveChangesAsync();

            return updatedTest.Id;
        }
    }
}
