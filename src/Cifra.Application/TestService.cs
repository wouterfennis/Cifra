using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
using Cifra.Application.Models.Results;
using Cifra.Application.Models.Commands;

namespace Cifra.Application
{
    /// <inheritdoc/>
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        /// <inheritdoc/>
        public async Task<CreateTestResult> CreateTestAsync(CreateTestCommand model)
        {
            var test = Test.TryCreate(model.Name, model.StandardizationFactor, model.MinimumGrade, model.NumberOfVersions);

            if (!test.IsSuccess)
            {
                return new CreateTestResult(test.ValidationMessage);
            }

            int id = await _testRepository.CreateAsync(test.Value);

            return new CreateTestResult(id);
        }

        /// <inheritdoc/>
        public async Task<UpdateTestResult> UpdateTestAsync(UpdateTestCommand model)
        {
            var test = Test.TryCreate(model.Test.Name, model.Test.StandardizationFactor, model.Test.MinimumGrade, model.Test.NumberOfVersions);

            if (!test.IsSuccess)
            {
                return new UpdateTestResult(test.ValidationMessage);
            }

            int id = await _testRepository.UpdateAsync(test.Value);

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

        /// <inheritdoc/>
        public async Task<DeleteTestResult> DeleteTestAsync(DeleteTestCommand command)
        {
            var name = Name.CreateFromString(command.Name);

            if (!name.IsSuccess)
            {
                return new DeleteTestResult(name.ValidationMessage);
            }

            await _testRepository.DeleteAsync(name.Value);
            return new DeleteTestResult();
        }
    }
}
