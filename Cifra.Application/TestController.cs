using Cifra.Application.Extensions;
using Cifra.Application.Interfaces;
using Cifra.Application.Models;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application
{
    public class TestController
    {
        private readonly ITestRepository _testRepository;
        private readonly IValidator<CreateTestRequest> _testValidator;
        private readonly IValidator<AddQuestionRequest> _questionValidator;

        public TestController(ITestRepository testRepository, 
            IValidator<CreateTestRequest> testValidator, 
            IValidator<AddQuestionRequest> questionValidator)
        {
            _testRepository = testRepository;
            _testValidator = testValidator;
            _questionValidator = questionValidator;
        }

        public CreateTestResult CreateTest(CreateTestRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _testValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateTestResult(validationMessages);
            }

            var test = new Models.Test.Test(Name.CreateFromString(model.Name), StandardizationFactor.CreateFromByte(model.StandardizationFactor), Grade.CreateFromByte(model.MinimumGrade));
            ValidationMessage result = _testRepository.Create(test);

            if(result != null)
            {
                return new CreateTestResult(result);
            }

            return new CreateTestResult(test);
        }

        public AddQuestionResult AddQuestion(AddQuestionRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _questionValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new AddQuestionResult(validationMessages);
            }

            var test = _testRepository.Get(model.TestId);
            if(test == null)
            {
                return new AddQuestionResult(new ValidationMessage(nameof(model.TestId), "No test was found"));
            }

            var names = model.Names.ToNames();
            var question = new Question(names, QuestionScore.CreateFromByte(model.MaximalScore));

            test.AddQuestion(question);
            ValidationMessage result = _testRepository.Update(test);

            if (result != null)
            {
                return new AddQuestionResult(result);
            }

            return new AddQuestionResult();
        }
    }
}
