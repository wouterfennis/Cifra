using Cifra.Application.Models.Validation;
using System.Collections.Generic;

namespace Cifra.Application.Validation
{
    /// <inheritdoc/>
    public class Validator<T> : IValidator<T>
    {
        private readonly IEnumerable<IValidationRule<T>> validationRules;

        /// <summary>
        /// Ctor
        /// </summary>
        public Validator(IEnumerable<IValidationRule<T>> validationRules)
        {
            this.validationRules = validationRules;
        }

        /// <inheritdoc/>
        public IEnumerable<ValidationMessage> ValidateRules(T model)
        {
            var validationMessages = new List<ValidationMessage>();

            foreach (var validationRule in validationRules)
            {
                var result = validationRule.Validate(model);
                if (result != null)
                {
                    validationMessages.Add(result);
                }
            }

            return validationMessages;
        }
    }
}
