using Cifra.Application.Models.Class.Requests;
using System;

namespace Cifra.Application.Validation.ClassModelValidationRules
{
    /// <summary>
    /// Validation rule to check name of the class
    /// </summary>
    public class NameMustBeFilled : IValidationRule<CreateClassRequest>
    {
        private const string Message = "Name is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateClassRequest model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                return new ValidationMessage(nameof(model.Name), Message);
            }
            return null;
        }

        private void NullChecks(CreateClassRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
