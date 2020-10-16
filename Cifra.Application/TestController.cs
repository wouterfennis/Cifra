using Cifra.Application.Extensions;
using Cifra.Application.Interfaces;
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

            var test = new Test(Name.CreateFromString(model.Name), StandardizationFactor.CreateFromByte(model.StandardizationFactor), Grade.CreateFromByte(model.MinimumGrade));
            Guid testId = _testRepository.Create(test);

            return new CreateTestResult(testId);
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
