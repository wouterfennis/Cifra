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
    public class ClassDatabaseRepository : IClassRepository
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        public ClassDatabaseRepository(Context dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(Domain.Class newClass)
        {
            _ = newClass ?? throw new ArgumentNullException(nameof(newClass));

            var entity = _mapper.Map<Class>(newClass);

            _dbContext.Classes.Add(entity);
            await _dbContext.SaveChangesAsync();

            return newClass.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Domain.Class>> GetAllAsync()
        {
            List<Class> entities = await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .ToListAsync();
            return _mapper.Map<List<Domain.Class>>(entities);
        }

        /// <inheritdoc/>
        public async Task<Domain.Class> GetAsync(int id)
        {
            Class findResult = await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<Domain.Class>(findResult);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Domain.Class updatedClass)
        {
            var entity = _mapper.Map<Class>(updatedClass);
            _dbContext.Attach(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
