using Cifra.Application.Models;
using Cifra.Application.Models.Test.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation.QuestionModelValidationRules
{
    /// <summary>
    /// Validates the test id of a question
    /// </summary>
    public class TestIdMustBeFilled : IValidationRule<AddQuestionRequest>
    {
        private const string Message = "TestId is required";

        /// <inheritdoc/>
        public ValidationMessage Validate(AddQuestionRequest model)
        {
            NullChecks(model);

            if (model.TestId == Guid.Empty)
            {
                return new ValidationMessage(nameof(model.TestId), Message);
            }
            return null;
        }

        private void NullChecks(AddQuestionRequest model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
