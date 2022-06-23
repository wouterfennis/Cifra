using Cifra.Core.Models.Validation;
using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database.Repositories
{
    /// <inheritdoc/>
    public class ClassDatabaseRepository : IClassRepository
    {
        private readonly Context _dbContext;

        public ClassDatabaseRepository(Context dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(Schema.Class newClass)
        {
            _ = newClass ?? throw new ArgumentNullException(nameof(newClass));
            
            _dbContext.Classes.Add(newClass);
            await _dbContext.SaveChangesAsync();
            
            return newClass.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Class>> GetAllAsync()
        {
            List<Class> entities = await _dbContext.Classes
                .Include(x => x.Students)
                .ToListAsync();
            return entities;
        }

        /// <inheritdoc/>
        public async Task<Class> GetAsync(int id)
        {
            Class findResult = await _dbContext.Classes
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);

            return findResult;
        }

        /// <inheritdoc/>
        public async Task<ValidationMessage> UpdateAsync(Class updatedClass)
        {
            _dbContext.Classes.Update(updatedClass);
            await _dbContext.SaveChangesAsync();
            return null;
        }
    }
}
