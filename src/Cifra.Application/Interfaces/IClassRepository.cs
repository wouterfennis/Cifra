using Cifra.Application.Models.Class;
using Cifra.Application.Models.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Repository for Classes.
    /// </summary>
    public interface IClassRepository
    {
        /// <summary>
        /// Retrieves a <see cref="Class"/>.
        /// </summary>
        Task<Class> GetAsync(Guid id);

        /// <summary>
        /// Create a <see cref="Class"/>.
        /// </summary>
        Task CreateAsync(Class @newClass);

        /// <summary>
        /// Updates a <see cref="Class"/>.
        /// </summary>
        Task<ValidationMessage> UpdateAsync(Class @class);

        /// <summary>
        /// Get all a <see cref="Class"/>es.
        /// </summary>
        Task<IEnumerable<Class>> GetAllAsync();
    }
}
