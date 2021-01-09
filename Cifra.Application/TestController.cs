using Cifra.Application.Extensions;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application
{
    /// <summary>
    /// Application Controller for the Test entity
    /// </summary>
    public class TestController
    {
        private readonly ITestRepository _testRepository;
        private readonly IValidator<CreateTestRequest> _testValidator;
        private readonly IValidator<AddAssignmentRequest> _assignmentValidator;
        private readonly IValidator<AddQuestionRequest> _questionValidator;


        /// <summary>
        /// Ctor
        /// </summary>
        public TestController(ITestRepository testRepository,
            IValidator<CreateTestRequest> testValidator,
            IValidator<AddAssignmentRequest> assignmentValidator,
            IValidator<AddQuestionRequest> questionValidator)
        {
            _testRepository = testRepository;
            _testValidator = testValidator;
            _assignmentValidator = assignmentValidator;
            _questionValidator = questionValidator;
        }

        /// <summary>
        /// Creates a test
        /// </summary>

        public async Task<CreateTestResult> CreateTestAsync(CreateTestRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _testValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateTestResult(validationMessages);
            }

            var test = new Test(Name.CreateFromString(model.Name), StandardizationFactor.CreateFromByte(model.StandardizationFactor), Grade.CreateFromByte(model.MinimumGrade));
            await _testRepository.CreateAsync(test);

            return new CreateTestResult(test.Id);
        }


        /// <summary>
        /// Adds an assignment to a test
        /// </summary>
        public async Task<AddAssignmentResult> AddAssignmentAsync(AddAssignmentRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _assignmentValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new AddAssignmentResult(validationMessages);
            }

            var test = await _testRepository.GetAsync(model.TestId);
            if (test == null)
            {
                return new AddAssignmentResult(new ValidationMessage(nameof(model.TestId), "No test was found"));
            }

            var assignment = new Assignment();

            test.AddAssignment(assignment);
            ValidationMessage result = await _testRepository.UpdateAsync(test);

            if (result != null)
            {
                return new AddAssignmentResult(result);
            }

            return new AddAssignmentResult(test.Id, assignment.Id);
        }

        /// <summary>
        /// Adds a question to a assignment
        /// </summary>
        public async Task<AddQuestionResult> AddQuestionAsync(AddQuestionRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _questionValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new AddQuestionResult(validationMessages);
            }

            var test = await _testRepository.GetAsync(model.TestId);
            if (test == null)
            {
                return new AddQuestionResult(new ValidationMessage(nameof(model.TestId), "No test was found"));
            }

            Assignment assignment = test.GetAssignment(model.AssignmentId);

            if (assignment == null)
            {
                return new AddQuestionResult(new ValidationMessage(nameof(model.AssignmentId), "No assignment was found"));
            }

            var names = model.Names.ToNames();
            var question = new Question(names, QuestionScore.CreateFromByte(model.MaximumScore));

            assignment.AddQuestion(question);
            ValidationMessage result = await _testRepository.UpdateAsync(test);

            if (result != null)
            {
                return new AddQuestionResult(result);
            }

            return new AddQuestionResult();
        }

        /// <summary>
        /// Retrieves all tests currently available
        /// </summary>
        public async Task<GetAllTestsResult> GetTestsAsync()
        {
            var tests = await _testRepository.GetAllAsync();
            return new GetAllTestsResult(tests);
        }
    }
}
