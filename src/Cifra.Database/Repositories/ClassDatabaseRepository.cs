using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using System.Linq;

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
        public async Task<uint> CreateAsync(Domain.Class newClass)
        {
            _ = newClass ?? throw new ArgumentNullException(nameof(newClass));

            _dbContext.Classes.Add(newClass);
            await _dbContext.SaveChangesAsync();

            return newClass.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Domain.Class>> GetAllAsync()
        {
            return await _dbContext.Classes
                .AsNoTracking()
                .Include(x => x.Students)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Domain.Class?> GetAsync(uint id)
        {
            return await _dbContext.Classes
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task<uint> UpdateAsync(Domain.Class updatedClass)
        {
            _dbContext.Classes.Update(updatedClass);

            await _dbContext.SaveChangesAsync();

            return updatedClass.Id;
        }
    }
}
