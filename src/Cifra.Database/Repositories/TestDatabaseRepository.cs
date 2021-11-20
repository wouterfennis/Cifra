using AutoMapper;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database.Repositories
{
    public class TestDatabaseRepository : ITestRepository
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        public TestDatabaseRepository(Context dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> CreateAsync(Test newTest)
        {
            _ = newTest ?? throw new ArgumentNullException(nameof(newTest));
            Schema.Test mappedTest = _mapper.Map<Schema.Test>(newTest);
            
            _dbContext.Add(mappedTest);
            await _dbContext.SaveChangesAsync();
            
            return mappedTest.Id;
        }

        public async Task<List<Test>> GetAllAsync()
        {
            List<Schema.Test> entities = await _dbContext.Tests.ToListAsync();
            return _mapper.Map<List<Test>>(entities);
        }

        public async Task<Test> GetAsync(int id)
        {
            var findResult = await _dbContext.Tests.FindAsync(id);
            return _mapper.Map<Test>(findResult);
        }

        public async Task<ValidationMessage> UpdateAsync(Test test)
        {
            _dbContext.Update(test);
            await _dbContext.SaveChangesAsync();
            return null;
        }
    }
}
