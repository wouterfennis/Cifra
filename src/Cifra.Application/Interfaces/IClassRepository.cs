using Cifra.Domain;
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
        Task<Class> GetAsync(uint id);

        /// <summary>
        /// Create a <see cref="Class"/>.
        /// </summary>
        Task<uint> CreateAsync(Class newClass);

        /// <summary>
        /// Updates a <see cref="Class"/>.
        /// </summary>
        Task<uint> UpdateAsync(Class updatedClass);

        /// <summary>
        /// Get all a <see cref="Class"/>es.
        /// </summary>
        Task<List<Class>> GetAllAsync();
    }
}
