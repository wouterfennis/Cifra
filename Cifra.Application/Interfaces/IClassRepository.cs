using Cifra.Application.Models.Class;
using Cifra.Application.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    /// <summary>
    /// Repository for Classes
    /// </summary>
    public interface IClassRepository
    {
        /// <summary>
        /// Retrieves a class
        /// </summary>
        Task<Class> GetAsync(Guid id);

        /// <summary>
        /// Create a class 
        /// </summary>
        Task CreateAsync(Class @class);

        /// <summary>
        /// Updates a class
        /// </summary>
        Task<ValidationMessage> UpdateAsync(Class @class);

        /// <summary>
        /// Get all classes
        /// </summary>
        Task<IEnumerable<Class>> GetAllAsync();
    }
}
