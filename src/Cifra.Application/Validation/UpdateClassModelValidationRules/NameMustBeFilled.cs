using Cifra.Application.Models.Class.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.UpdateClassModelValidationRules
{
    /// <summary>
    /// Validation rule to check name of the class
    /// </summary>
    public class NameMustBeFilled : IValidationRule<UpdateClassCommand>
    {
        private const string Message = "Name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(UpdateClassCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.UpdatedClass.Name) || string.IsNullOrWhiteSpace(model.UpdatedClass.Name))
            {
                return new ValidationMessage(nameof(model.UpdatedClass.Name), Message);
            }
            return null;
        }

        private void NullChecks(UpdateClassCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
