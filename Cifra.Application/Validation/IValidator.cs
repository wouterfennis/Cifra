using Cifra.Application.Models;
using System.Collections;
using System.Collections.Generic;

namespace Cifra.Application.Validation
{
    public interface IValidator<T>
    {
        IEnumerable<ValidationMessage> ValidateRules(T model);
    }
}