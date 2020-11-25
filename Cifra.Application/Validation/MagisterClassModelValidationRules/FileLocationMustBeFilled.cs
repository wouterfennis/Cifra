using Cifra.Application.Models.Class.Requests;
using System;

namespace Cifra.Application.Validation.MagisterClassModelValidationRules
{
    public class FileLocationMustBeFilled : IValidationRule<CreateMagisterClassRequest>
    {
        private const string Message = "File location is required";
        public ValidationMessage Validate(CreateMagisterClassRequest model)
        {
            NullChecks(model);

            if (string.IsNullOrEmpty(model.MagisterFileLocation) || string.IsNullOrWhiteSpace(model.MagisterFileLocation))
            {
                return new ValidationMessage(nameof(model.MagisterFileLocation), Message);
            }
            return null;
        }

        private void NullChecks(CreateMagisterClassRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
