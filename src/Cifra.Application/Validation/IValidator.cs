using Cifra.Domain.Validation;
using System.Collections.Generic;

namespace Cifra.Application.Validation
{
    /// <summary>
    /// Validator the run certain validation rules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Execute a certain set of validation rules on a model
        /// </summary>
        IEnumerable<ValidationMessage> ValidateRules(T model);
    }
}