using Cifra.Application.Models.Class.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.CreateClassModelValidationRules
{
    /// <summary>
    /// Validation rule to check name of the class
    /// </summary>
    public class NameMustBeFilled : IValidationRule<CreateClassCommand>
    {
        private const string Message = "Name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateClassCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                return new ValidationMessage(nameof(model.Name), Message);
            }
            return null;
        }

        private void NullChecks(CreateClassCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
