using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using System.Linq;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;

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
        public async Task<uint> CreateAsync(Class newClass)
        {
            _ = newClass ?? throw new ArgumentNullException(nameof(newClass));

            _dbContext.Classes.Add(newClass);
            await _dbContext.SaveChangesAsync();

            return newClass.Id;
        }

        public async Task DeleteAsync(Class classToBeDeleted)
        {
            _ = classToBeDeleted ?? throw new ArgumentNullException(nameof(classToBeDeleted));

            var foundClass = await GetAsync(classToBeDeleted.Name);

            _dbContext.Classes.Remove(foundClass);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Class>> GetAllAsync()
        {
            return await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Class?> GetAsync(uint id)
        {
            return await _dbContext.Classes
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        private async Task<Class> GetAsync(Name name)
        {
            return await _dbContext.Classes
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Name == name);
        }

        /// <inheritdoc/>
        public async Task<uint> UpdateAsync(Class updatedClass)
        {
            _dbContext.Classes.Update(updatedClass);

            await _dbContext.SaveChangesAsync();

            return updatedClass.Id;
        }
    }
}
