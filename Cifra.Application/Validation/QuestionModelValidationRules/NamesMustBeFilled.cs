using Cifra.Application.Models.Test.Requests;
using System;

namespace Cifra.Application.Validation.QuestionModelValidationRules
{
    /// <summary>
    /// Validates the names of question
    /// </summary>
    public class NamesMustBeFilled : IValidationRule<AddQuestionRequest>
    {
        private const string Message = "Not all names are valid";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddQuestionRequest model)
        {
            NullChecks(model);

            foreach (string name in model.Names)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                {
                    return new ValidationMessage(nameof(name), Message);
                }
            }
            return null;
        }

        private void NullChecks(AddQuestionRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Names == null)
            {
                throw new ArgumentNullException(nameof(model.Names));
            }
        }
    }
}
