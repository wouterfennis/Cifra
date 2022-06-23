using AutoMapper;
using Cifra.Application.Models.Spreadsheet.Commands;
using Cifra.Application.Models.Spreadsheet.Results;
using Cifra.Core.Models.Spreadsheet;
using Cifra.Core.Models.Validation;
using Cifra.Database.Repositories;
using Cifra.Database.Schema;
using Cifra.FileSystem.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
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
            var validationMessages = new List<ValidationMessage>();
            Class pickedClass = await _classRepository.GetAsync(command.ClassId);

            if (pickedClass == null)
            {
                validationMessages.Add(ValidationMessage.Create("Class", "Not found"));
            }

            Test pickedTest = await _testRepository.GetAsync(command.TestId);

            if (pickedTest == null)
            {
                validationMessages.Add(ValidationMessage.Create("Test", "Not found"));
            }

            if (validationMessages.Any())
            {
                return new CreateTestResultsSpreadsheetResult(validationMessages);
            }

            var mappedClass = _mapper.Map<Core.Models.Class.Class>(pickedClass);
            var mappedTest = _mapper.Map<Core.Models.Test.Test>(pickedTest);

            await _testResultsSpreadsheetBuilder.CreateTestResultsSpreadsheetAsync(mappedClass, mappedTest, command.Metadata);

            return new CreateTestResultsSpreadsheetResult("something");
        }
    }
}
