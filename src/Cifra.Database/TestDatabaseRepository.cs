using AutoMapper;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database
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

        public Task CreateAsync(Test newTest)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            List<Schema.Test> entities = await _dbContext.Tests.ToListAsync();
            return _mapper.Map<IEnumerable<Test>>(entities);
        }

        public Task<Test> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationMessage> UpdateAsync(Test test)
        {
            throw new NotImplementedException();
        }
    }
}
