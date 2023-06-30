using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Domain.Validation;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;

namespace Cifra.Application
{
    /// <inheritdoc/>
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IValidator<CreateTestCommand> _createTestValidator;
        private readonly IValidator<UpdateTestCommand> _updateTestValidator;
        private readonly IValidator<AddAssignmentCommand> _assignmentValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestService(ITestRepository testRepository,
            IValidator<CreateTestCommand> createTestValidator,
            IValidator<UpdateTestCommand> updateTestValidator,
            IValidator<AddAssignmentCommand> assignmentValidator)
        {
            _testRepository = testRepository;
            _createTestValidator = createTestValidator;
            _updateTestValidator = updateTestValidator;
            _assignmentValidator = assignmentValidator;
        }

        /// <inheritdoc/>
        public async Task<CreateTestResult> CreateTestAsync(CreateTestCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _createTestValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new CreateTestResult(validationMessages);
            }

            var test = new Test(Name.CreateFromString(model.Name),
                StandardizationFactor.CreateFromInteger(model.StandardizationFactor),
                Grade.CreateFromInteger(model.MinimumGrade),
                model.NumberOfVersions);

            int id = await _testRepository.CreateAsync(test);

            return new CreateTestResult(id);
        }

        /// <inheritdoc/>
        public async Task<AddAssignmentResult> AddAssignmentAsync(AddAssignmentCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _assignmentValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new AddAssignmentResult(validationMessages);
            }

            Test test = await _testRepository.GetAsync(model.TestId);
            if (test == null)
            {
                return new AddAssignmentResult(new ValidationMessage(nameof(model.TestId), "No test was found"));
            }

            var assignment = new Assignment(model.NumberOfQuestions);
            test.Assignments.Add(assignment);

            await _testRepository.UpdateAsync(test);

            return new AddAssignmentResult(test.Id, assignment.Id);
        }

        /// <inheritdoc/>
        public async Task<UpdateTestResult> UpdateTestAsync(UpdateTestCommand model)
        {
            IEnumerable<ValidationMessage> validationMessages = _updateTestValidator.ValidateRules(model);
            if (validationMessages.Any())
            {
                return new UpdateTestResult(validationMessages);
            }

            int id = await _testRepository.UpdateAsync(model.Test);

            return new UpdateTestResult(id);
        }

        /// <inheritdoc/>
        public async Task<GetAllTestsResult> GetTestsAsync()
        {
            List<Test> tests = await _testRepository.GetAllAsync();
            return new GetAllTestsResult(tests);
        }

        /// <inheritdoc/>
        public async Task<GetTestResult> GetTestAsync(int id)
        {
            Test test = await _testRepository.GetAsync(id);
            return new GetTestResult(test);
        }
    }
}
