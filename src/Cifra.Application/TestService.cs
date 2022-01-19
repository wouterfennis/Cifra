using AutoMapper;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Validation;
using Cifra.Core.Models.Test;
using Cifra.Core.Models.Validation;
using Cifra.Core.Models.ValueTypes;
using Cifra.Database.Repositories;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestService(ITestRepository testRepository,
            IValidator<CreateTestCommand> testValidator,
            IValidator<AddAssignmentCommand> assignmentValidator,
            IMapper mapper)
        {
            _testRepository = testRepository;
            _testValidator = testValidator;
            _assignmentValidator = assignmentValidator;
            _mapper = mapper;
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

            var mappedTest = _mapper.Map<Database.Schema.Test>(test);

            int id = await _testRepository.CreateAsync(mappedTest);

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

            Database.Schema.Test test = await _testRepository.GetAsync(model.TestId);
            var mappedTest = _mapper.Map<Test>(test);
            if (test == null)
            {
                return new AddAssignmentResult(new ValidationMessage(nameof(model.TestId), "No test was found"));
            }
            
            var assignment = new Assignment(model.NumberOfQuestions);
            mappedTest.AddAssignment(assignment);

            var updatedTest = _mapper.Map<Database.Schema.Test>(test);

            ValidationMessage result = await _testRepository.UpdateAsync(updatedTest);

            if (result != null)
            {
                return new AddAssignmentResult(result);
            }

            return new AddAssignmentResult(test.Id, assignment.Id);
        }

        /// <inheritdoc/>
        public async Task<GetAllTestsResult> GetTestsAsync()
        {
            List<Database.Schema.Test> tests = await _testRepository.GetAllAsync();
            var mappedTests = _mapper.Map<List<Test>>(tests);
            return new GetAllTestsResult(mappedTests);
        }
    }
}
