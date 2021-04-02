using Cifra.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation
{
    /// <summary>
    /// Validation rule for <c>T</c>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// Validates model
        /// </summary>
        ValidationMessage Validate(T model);
    }
}
