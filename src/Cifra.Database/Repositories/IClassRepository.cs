using Cifra.Core.Models.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Database.Repositories
{
    /// <summary>
    /// Repository for Classes.
    /// </summary>
    public interface IClassRepository
    {
        /// <summary>
        /// Retrieves a <see cref="Class"/>.
        /// </summary>
        Task<Schema.Class> GetAsync(int id);

        /// <summary>
        /// Create a <see cref="Class"/>.
        /// </summary>
        Task<int> CreateAsync(Schema.Class newClass);

        /// <summary>
        /// Updates a <see cref="Class"/>.
        /// </summary>
        Task<ValidationMessage> UpdateAsync(Schema.Class updatedClass);

        /// <summary>
        /// Get all a <see cref="Class"/>es.
        /// </summary>
        Task<List<Schema.Class>> GetAllAsync();
    }
}
