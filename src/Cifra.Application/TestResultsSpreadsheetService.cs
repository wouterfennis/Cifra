using Cifra.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifra.Application.Interfaces;
using Cifra.Domain;
using Cifra.Application.Models.Results;
using Cifra.Commands;
using Cifra.Domain.Spreadsheet;

namespace Cifra.Application
{
    internal class TestResultsSpreadsheetService : ITestResultsSpreadsheetService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITestRepository _testRepository;
        private readonly ITestResultsSpreadsheetBuilder _testResultsSpreadsheetBuilder;

        /// <summary>
        /// Ctor
        /// </summary>
        public TestResultsSpreadsheetService(IClassRepository classRepository,
            ITestRepository testRepository,
            ITestResultsSpreadsheetBuilder testResultsSpreadsheetBuilder)
        {
            _classRepository = classRepository;
            _testRepository = testRepository;
            _testResultsSpreadsheetBuilder = testResultsSpreadsheetBuilder;
        }

        /// <inheritdoc/>
        public async Task<CreateTestResultsSpreadsheetResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetCommand command)
        {
            var validationMessages = new List<ValidationMessage>();
            Class pickedClass = await _classRepository.GetAsync(command.ClassId);
            Result<Metadata> metadata = Metadata.TryCreate(command.Metadata.Author, command.Metadata.Title, command.Metadata.Subject, command.Metadata.Created, command.Metadata.FileName, command.Metadata.ApplicationVersion);

            if (!metadata.IsSuccess)
            {
                validationMessages.Add(metadata.ValidationMessage);
            }

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

            var fileInfo = await _testResultsSpreadsheetBuilder.CreateTestResultsSpreadsheetAsync(pickedClass, pickedTest, metadata.Value);

            return new CreateTestResultsSpreadsheetResult(fileInfo);
        }
    }
}
