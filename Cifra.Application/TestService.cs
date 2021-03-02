using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;

namespace Cifra.Application
{
    /// <inheritdoc/>
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IValidator<CreateTestRequest> _testValidator;
        private readonly IValidator<AddAssignmentRequest> _assignmentValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestService(ITestRepository testRepository,
            IValidator<CreateTestRequest> testValidator,
            IValidator<AddAssignmentRequest> assignmentValidator)
        {
            _testRepository = testRepository;
            _testValidator = testValidator;
            _assignmentValidator = assignmentValidator;
        }

        /// <inheritdoc/>
        public async Task<CreateTestResult> CreateTestAsync(CreateTestRequest model)
        {
            IEnumerable<ValidationMessage> validationMessages = _testValidator.ValidateRules(model);
            if (validationMessages.Count() > 0)
            {
                return new CreateTestResult(validationMessages);
            }

            var test = new Test(Name.CreateFromString(model.Name),
                StandardizationFactor.CreateFromByte(model.StandardizationFactor),
                Grade.CreateFromByte(model.MinimumGrade),
                model.NumberOfVersions);
            await _testRepository.CreateAsync(test);

            return new CreateTestResult(test.Id);
        }


        /// <inheritdoc/>
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

            var assignment = new Assignment(model.NumberOfQuestions);

            test.AddAssignment(assignment);
            ValidationMessage result = await _testRepository.UpdateAsync(test);

            if (result != null)
            {
                return new AddAssignmentResult(result);
            }

            return new AddAssignmentResult(test.Id, assignment.Id);
        }

        /// <inheritdoc/>
        public async Task<GetAllTestsResult> GetTestsAsync()
        {
            var tests = await _testRepository.GetAllAsync();
            return new GetAllTestsResult(tests);
        }
    }
}
