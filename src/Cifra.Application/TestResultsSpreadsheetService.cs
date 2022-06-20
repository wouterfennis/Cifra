using AutoMapper;
using Cifra.Application.Models.Spreadsheet.Commands;
using Cifra.Application.Models.Spreadsheet.Results;
using Cifra.Core.Models.Spreadsheet;
using Cifra.Database.Repositories;
using Cifra.Database.Schema;
using Cifra.FileSystem.Spreadsheet;
using System.Threading.Tasks;

namespace Cifra.Application
{
    internal class TestResultsSpreadsheetService : ITestResultsSpreadsheetService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITestRepository _testRepository;
        private readonly ITestResultsSpreadsheetBuilder _testResultsSpreadsheetBuilder;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestResultsSpreadsheetService(IClassRepository classRepository,
            ITestRepository testRepository,
            ITestResultsSpreadsheetBuilder testResultsSpreadsheetBuilder,
            IMapper mapper)
        {
            _classRepository = classRepository;
            _testRepository = testRepository;
            _testResultsSpreadsheetBuilder = testResultsSpreadsheetBuilder;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<CreateTestResultsSpreadsheetResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetCommand command)
        {
            Class pickedClass = await _classRepository.GetAsync(command.ClassId);
            var mappedClass = _mapper.Map<Core.Models.Class.Class>(pickedClass);

            Test pickedTest = await _testRepository.GetAsync(command.TestId);
            var mappedTest = _mapper.Map<Core.Models.Test.Test>(pickedTest);

            await _testResultsSpreadsheetBuilder.CreateTestResultsSpreadsheetAsync(mappedClass, mappedTest, command.Metadata);

            return new CreateTestResultsSpreadsheetResult("something");
        }
    }
}
