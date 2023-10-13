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
    public class ClassDatabaseRepository : IClassRepository
    {
        private readonly Context _dbContext;

        public ClassDatabaseRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(Domain.Class newClass)
        {
            _ = newClass ?? throw new ArgumentNullException(nameof(newClass));

            var entity = newClass.MapToSchema();

            _dbContext.Classes.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Domain.Class>> GetAllAsync()
        {
            List<Class> entities = await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .ToListAsync();
            return entities.MapToDomain();
        }

        /// <inheritdoc/>
        public async Task<Domain.Class> GetAsync(int id)
        {
            Class findResult = await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);

            return findResult.MapToDomain();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateAsync(Domain.Class updatedClass)
        {
            var updatedEntity = updatedClass.MapToSchema(); 
            var existingEntity = await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == updatedClass.Id);
            var studentsToRemove = existingEntity.Students
                .Where(x => !updatedEntity.Students.Select(y => y.Id)
                .Contains(x.Id))
                .ToList();

            _dbContext.Classes.Update(updatedEntity);
            _dbContext.Students.RemoveRange(studentsToRemove);

            await _dbContext.SaveChangesAsync();

            return updatedEntity.Id;
        }
    }
}
