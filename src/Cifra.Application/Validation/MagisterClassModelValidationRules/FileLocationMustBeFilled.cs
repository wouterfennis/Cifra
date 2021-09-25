using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Validation;
using System;

namespace Cifra.Application.Validation.MagisterClassModelValidationRules
{
    /// <summary>
    /// Validation rule to check the file location of a magister class.
    /// </summary>
    public class FileLocationMustBeFilled : IValidationRule<CreateMagisterClassCommand>
    {
        private const string Message = "File location is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(CreateMagisterClassCommand model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.MagisterFileLocation) || string.IsNullOrWhiteSpace(model.MagisterFileLocation))
            {
                return new ValidationMessage(nameof(model.MagisterFileLocation), Message);
            }
            return null;
        }

        private void NullChecks(CreateMagisterClassCommand model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
