using Cifra.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation
{
    public interface IValidationRule<T>
    {
        ValidationMessage Validate(T model);
    }
}
