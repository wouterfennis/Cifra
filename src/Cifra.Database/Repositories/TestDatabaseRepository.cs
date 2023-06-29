using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cifra.Application.Interfaces;

namespace Cifra.Database.Repositories
{
    /// <inheritdoc/>
    public class TestDatabaseRepository : ITestRepository
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        public TestDatabaseRepository(Context dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(Domain.Test newTest)
        {
            _ = newTest ?? throw new ArgumentNullException(nameof(newTest));

            var entity = _mapper.Map<Test>(newTest);

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
            return _mapper.Map<List<Domain.Test>>(entities); 
        }

        /// <inheritdoc/>
        public async Task<Domain.Test> GetAsync(int id)
        {
            Test findResult = await _dbContext.Tests
                .AsNoTracking()
                .Include(x => x.Assignments)
                .SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Domain.Test>(findResult); 
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Domain.Test updatedTest)
        {
            var entity = _mapper.Map<Test>(updatedTest);

            _dbContext.Attach(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
