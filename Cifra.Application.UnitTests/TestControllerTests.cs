using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Class;
using System.Linq;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;

namespace Cifra.Application.UnitTests
{
    [TestClass]
    public class TestControllerTests
    {
        private Fixture _fixture;
        private Mock<ITestRepository> _testRepository;
        private Mock<IValidator<CreateTestRequest>> _testValidator;
        private Mock<IValidator<AddQuestionRequest>> _questionValidator;
        private TestController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testRepository = new Mock<ITestRepository>();
            _testValidator = new Mock<IValidator<CreateTestRequest>>();
            _questionValidator = new Mock<IValidator<AddQuestionRequest>>();
            _sut = new TestController(_testRepository.Object, _testValidator.Object, _questionValidator.Object);
        }

        [TestMethod]
        public void CreateTest_WithValidRequest_CreatesClass()
        {
            CreateTestRequest input = CreateDefaultTestRequest();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            var expectedTestId = _fixture.Create<Guid>();
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            _testRepository
                .Setup(x => x.Create(It.Is<Test>(x => x.Name.Value == input.Name)))
                .Returns(expectedTestId);

            CreateTestResult result = _sut.CreateTest(input);

            result.TestId.Should().Be(expectedTestId);
            result.ValidationMessages.Should().BeEmpty();
        }

        [TestMethod]
        public void CreateTest_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = CreateDefaultTestRequest();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateTestResult result = _sut.CreateTest(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public void CreateTest_WithValidationMessages_DoesNotCreateClass()
        {
            var input = CreateDefaultTestRequest();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateTestResult result = _sut.CreateTest(input);

            _testRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void AddQuestion_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddQuestionResult result = _sut.AddQuestion(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public void AddQuestion_WithValidationMessages_DoesNotAddStudent()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddQuestionResult result = _sut.AddQuestion(input);

            _testRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void AddQuestion_ClassDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            _testRepository
                .Setup(x => x.Get(input.TestId))
                .Returns((Test)null);

            AddQuestionResult result = _sut.AddQuestion(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("TestId");
            validationMessage.Message.Should().Be("No test was found");
        }

        [TestMethod]
        public void AddQuestion_UpdateFails_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddQuestionRequest>();
            var studentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _questionValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(studentValidationMessages);

            Test expectedClass = CreateDefaultTest();
            _testRepository
                .Setup(x => x.Get(input.TestId))
                .Returns(expectedClass);

            var testValidationMessage = _fixture.Create<ValidationMessage>();
            _testRepository
                .Setup(x => x.Update(expectedClass))
                .Returns(testValidationMessage);

            AddQuestionResult result = _sut.AddQuestion(input);

            result.ValidationMessages.Should().ContainSingle();
            result.ValidationMessages.Should().Contain(testValidationMessage);
        }

        private CreateTestRequest CreateDefaultTestRequest()
        {
            return _fixture.Build<CreateTestRequest>()
                .With(x => x.MinimumGrade, 5)
                .With(x => x.StandardizationFactor, 5)
                .Create();
        }

        private Test CreateDefaultTest()
        {
            return new Test(
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(5),
                Grade.CreateFromByte(4));
        }
    }
}
