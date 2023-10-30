using System.Collections.Generic;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
using Cifra.Application.Models.Results;
using Cifra.Commands;
using Cifra.Domain.Validation;
using System.Xml.Linq;

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
                return new CreateTestResult(test.ValidationMessage!);
            }

            uint id = await _testRepository.CreateAsync(test.Value!);

            return new CreateTestResult(id);
        }

        /// <inheritdoc/>
        public async Task<UpdateTestResult> UpdateTestAsync(UpdateTestCommand model)
        {
            var updatedTestResult = Test.TryCreate(model.Test.Name, model.Test.StandardizationFactor, model.Test.MinimumGrade, model.Test.NumberOfVersions);
            var originalTest = await _testRepository.GetAsync(model.Test.Id);

            if (!updatedTestResult.IsSuccess)
            {
                return new UpdateTestResult(updatedTestResult.ValidationMessage!);
            }

            if (originalTest is null)
            {
                return new UpdateTestResult(ValidationMessage.Create(nameof(model.Test.Id), "Test to update cannot be found"));
            }

            originalTest.UpdateFromOtherTest(updatedTestResult.Value!);

            uint id = await _testRepository.UpdateAsync(originalTest);

            return new UpdateTestResult(id);
        }

        /// <inheritdoc/>
        public async Task<GetAllTestsResult> GetTestsAsync()
        {
            List<Test> tests = await _testRepository.GetAllAsync();
            return new GetAllTestsResult(tests);
        }

        /// <inheritdoc/>
        public async Task<GetTestResult> GetTestAsync(uint id)
        {
            Test? test = await _testRepository.GetAsync(id);
            return new GetTestResult { Test = test };
        }

        /// <inheritdoc/>
        public async Task<DeleteTestResult> DeleteTestAsync(DeleteTestCommand command)
        {
            await _testRepository.DeleteAsync(command.TestId);

            return new DeleteTestResult();
        }
    }
}
