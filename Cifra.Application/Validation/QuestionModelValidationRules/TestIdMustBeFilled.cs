using Cifra.Application.Models;
using Cifra.Application.Models.Test.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Validation.QuestionModelValidationRules
{
    public class TestIdMustBeFilled : IValidationRule<AddQuestionRequest>
    {
        private const string Message = "TestId is required";
        public ValidationMessage Validate(AddQuestionRequest model)
        {
            if (model.TestId == Guid.Empty)
            {
                return new ValidationMessage(nameof(model.TestId), Message);
            }
            return null;
        }
    }
}
