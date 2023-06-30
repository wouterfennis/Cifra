using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.UpdateTestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NameMustBeFilled : IValidationRule<UpdateTestCommand>
    {
        private const string Message = "Name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(UpdateTestCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.Test.Name) || string.IsNullOrWhiteSpace(model.Test.Name))
            {
                return new ValidationMessage(nameof(model.Test.Name), Message);
            }
            return null;
        }

        private void NullChecks(UpdateTestCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
