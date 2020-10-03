using Cifra.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application
{
    public class Controller
    {
        public CreateTestResult CreateTest(CreateTestModel model)
        {
            IEnumerable<ValidationMessage> validationMessages = ValidateModel(model);
            if(validationMessages.Count() > 0)
            {
                return new CreateTestResult
                {
                    ValidationMessages = validationMessages
                };
            }

            var test = new Test(Name.CreateFromString(model.Name), StandardizationFactor.CreateFromByte(model.StandardizationFactor), Grade.CreateFromByte(model.MinimumGrade));
            // persist

            return new CreateTestResult()
            {
                Test = test,
            };
        }

        private IEnumerable<ValidationMessage> ValidateModel(CreateTestModel model)
        {
            var validationMessages = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.Name))
            {
                validationMessages.Add(new ValidationMessage(nameof(model.Name), "Name is invalid"));
            }

            return validationMessages;
        }
    }
}
