using Cifra.Application.Models.Test.Commands;
using Cifra.Domain.Validation;
using System;

namespace Cifra.Application.Validation.CreateTestModelValidationRules
{
    /// <summary>
    /// Validates the name of a test
    /// </summary>
    public class NameMustBeFilled : IValidationRule<CreateTestCommand>
    {
        private const string Message = "Name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateTestCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                return new ValidationMessage(nameof(model.Name), Message);
            }
            return null;
        }

        private void NullChecks(CreateTestCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
