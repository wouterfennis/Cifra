using Cifra.Application.Models.Class;
using Cifra.Application.Validation;
using System;

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
        Class Get(Guid id);

        /// <summary>
        /// Create a class 
        /// </summary>
        ValidationMessage Create(Class @class);

        /// <summary>
        /// Updates a class
        /// </summary>
        ValidationMessage Update(Class @class);
    }
}
