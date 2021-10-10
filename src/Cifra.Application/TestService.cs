using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.Validation;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application
{
    /// <inheritdoc/>
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IValidator<CreateTestCommand> _testValidator;
        private readonly IValidator<AddAssignmentCommand> _assignmentValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestService(ITestRepository testRepository,
            IValidator<CreateTestCommand> testValidator,
            IValidator<AddAssignmentCommand> assignmentValidator)
        {
            _testRepository = testRepository;
            _testValidator = testValidator;
            _assignmentValidator = assignmentValidator;
        }

        /// <inheritdoc/>
        public async Task<CreateTestResult> CreateTestAsync(CreateTestCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _testValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new CreateTestResult(validationMessages);
            }

            var test = new Test(Name.CreateFromString(model.Name),
                StandardizationFactor.CreateFromInteger(model.StandardizationFactor),
                Grade.CreateFromInteger(model.MinimumGrade),
                model.NumberOfVersions);
            await _testRepository.CreateAsync(test);

            return new CreateTestResult(test.Id);
        }

        /// <inheritdoc/>
        public async Task<AddAssignmentResult> AddAssignmentAsync(AddAssignmentCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _assignmentValidator.ValidateRules(model);
            if (validationMessages.Any())
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
