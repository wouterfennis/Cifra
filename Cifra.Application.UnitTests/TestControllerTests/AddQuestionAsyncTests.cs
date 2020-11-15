using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Cifra.Application.UnitTests.TestControllerTests
{
    [TestClass]
    public class AddQuestionAsyncTests
    {
        private Fixture _fixture;
        private Mock<ITestRepository> _testRepository;
        private Mock<IValidator<CreateTestRequest>> _testValidator;
        private Mock<IValidator<AddQuestionRequest>> _questionValidator;
        private Mock<IValidator<AddAssignmentRequest>> _assignmentValidator;
        private TestController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testRepository = new Mock<ITestRepository>();
            _testValidator = new Mock<IValidator<CreateTestRequest>>();
            _assignmentValidator = new Mock<IValidator<AddAssignmentRequest>>();
            _questionValidator = new Mock<IValidator<AddQuestionRequest>>();
            _sut = new TestController(_testRepository.Object, _testValidator.Object, _assignmentValidator.Object, _questionValidator.Object);
        }

        [TestMethod]
        public async Task AddQuestion_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddQuestionResult result = await _sut.AddQuestionAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task AddQuestion_WithValidationMessages_DoesNotAddStudent()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddQuestionResult result = await _sut.AddQuestionAsync(input);

            _testRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddQuestion_TestDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync((Test)null);

            AddQuestionResult result = await _sut.AddQuestionAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("TestId");
            validationMessage.Message.Should().Be("No test was found");
        }

        [TestMethod]
        public async Task AddQuestion_AssignmentDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            Test expectedTest = CreateDefaultTest();
            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedTest);

            var testValidationMessage = _fixture.Create<ValidationMessage>();
            _testRepository
                .Setup(x => x.UpdateAsync(expectedTest))
                .ReturnsAsync(testValidationMessage);

            AddQuestionResult result = await _sut.AddQuestionAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("AssignmentId");
            validationMessage.Message.Should().Be("No assignment was found");
        }

        [TestMethod]
        public async Task AddQuestion_UpdateFails_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            Test expectedTest = CreateDefaultTest();
            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedTest);

            var testValidationMessage = _fixture.Create<ValidationMessage>();
            _testRepository
                .Setup(x => x.UpdateAsync(expectedTest))
                .ReturnsAsync(testValidationMessage);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            result.ValidationMessages.Should().Contain(testValidationMessage);
        }

        [TestMethod]
        public async Task AddQuestion_UpdateSucceeds_ReturnsResult()
        {
            Test expectedTest = CreateDefaultTest();
            var input = _fixture.Build<AddQuestionRequest>()
                .With(x => x.AssignmentId, expectedTest.Assignments.First().Id)
                .Create();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedTest);

            _testRepository
                .Setup(x => x.UpdateAsync(expectedTest))
                .ReturnsAsync((ValidationMessage)null);

            AddQuestionResult result = await _sut.AddQuestionAsync(input);

            result.ValidationMessages.Should().BeEmpty();
        }

        private Test CreateDefaultTest()
        {
            return new Test(Guid.NewGuid(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(5),
                Grade.CreateFromByte(4),
                CreateDefaultAssignments());
        }

        private List<Assignment> CreateDefaultAssignments()
        {
            return new List<Assignment>
            {
                new Assignment()
            };
        }
    }
}
