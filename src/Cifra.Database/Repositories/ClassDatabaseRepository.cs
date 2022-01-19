using Cifra.Core.Models.Validation;
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
        public async Task<List<Schema.Class>> GetAllAsync()
        {
            List<Schema.Class> entities = await _dbContext.Classes.ToListAsync();
            return entities;
        }

        /// <inheritdoc/>
        public async Task<Schema.Class> GetAsync(int id)
        {
            Schema.Class findResult = await _dbContext.Classes.FindAsync(id);
            return findResult;
        }

        /// <inheritdoc/>
        public async Task<ValidationMessage> UpdateAsync(Schema.Class updatedClass)
        {
            _dbContext.Classes.Update(updatedClass);
            await _dbContext.SaveChangesAsync();
            return new ValidationMessage("", "");
        }
    }
}
