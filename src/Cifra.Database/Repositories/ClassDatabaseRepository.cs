using AutoMapper;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database.Repositories
{
    public class ClassDatabaseRepository : IClassRepository
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        public ClassDatabaseRepository(Context dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> CreateAsync(Class newClass)
        {
            _ = newClass ?? throw new ArgumentNullException(nameof(newClass));
            Schema.Class mappedClass = _mapper.Map<Schema.Class>(newClass);
            
            _dbContext.Classes.Add(mappedClass);
            await _dbContext.SaveChangesAsync();
            
            return mappedClass.Id;
        }

        public async Task<List<Class>> GetAllAsync()
        {
            List<Schema.Class> entities = await _dbContext.Classes.ToListAsync();
            return _mapper.Map<List<Class>>(entities);
        }

        public async Task<Class> GetAsync(int id)
        {
            var findResult = await _dbContext.Classes.FindAsync(id);
            return _mapper.Map<Class>(findResult);
        }

        public async Task<ValidationMessage> UpdateAsync(Class updatedClass)
        {
            Schema.Class mappedClass = _mapper.Map<Schema.Class>(updatedClass);

            _dbContext.Classes.Update(mappedClass);
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
