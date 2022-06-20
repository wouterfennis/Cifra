using AutoMapper;
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
        public async Task<SaveResult> CreateTestResultsSpreadsheetAsync(int classId, int testId, Metadata metadata)
        {
            Class pickedClass = await _classRepository.GetAsync(classId);
            var mappedClass = _mapper.Map<Core.Models.Class.Class>(pickedClass);

            Test pickedTest = await _testRepository.GetAsync(testId);
            var mappedTest = _mapper.Map<Core.Models.Test.Test>(pickedTest);

            return await _testResultsSpreadsheetBuilder.CreateTestResultsSpreadsheetAsync(mappedClass, mappedTest, metadata);
        }
    }
}
