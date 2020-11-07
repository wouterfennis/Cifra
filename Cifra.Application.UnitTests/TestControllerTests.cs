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

namespace Cifra.Application.UnitTests
{
    [TestClass]
    public class TestControllerTests
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
        public async Task CreateTest_WithValidRequest_CreatesClass()
        {
            CreateTestRequest input = CreateDefaultTestRequest();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            CreateTestResult result = await _sut.CreateTestAsync(input);

            result.TestId.Should().NotBeEmpty();
            result.ValidationMessages.Should().BeEmpty();

            _testRepository
                .Verify(x => x.CreateAsync(It.Is<Test>(x => x.Name.Value == input.Name)));
        }

        [TestMethod]
        public async Task CreateTest_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = CreateDefaultTestRequest();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateTestResult result = await _sut.CreateTestAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task CreateTest_WithValidationMessages_DoesNotCreateClass()
        {
            var input = CreateDefaultTestRequest();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateTestResult result = await _sut.CreateTestAsync(input);

            _testRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddAssignment_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task AddAssignment_WithValidationMessages_DoesNotAddStudent()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            _testRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddAssignment_TestDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync((Test)null);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("TestId");
            validationMessage.Message.Should().Be("No test was found");
        }

        [TestMethod]
        public async Task AddAssignment_UpdateFails_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            Test expectedClass = CreateDefaultTest();
            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedClass);

            var testValidationMessage = _fixture.Create<ValidationMessage>();
            _testRepository
                .Setup(x => x.UpdateAsync(expectedClass))
                .ReturnsAsync(testValidationMessage);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            result.ValidationMessages.Should().Contain(testValidationMessage);
        }

        [TestMethod]
        public async Task GetAllTests_TestsAvailable_ReturnsTests()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            Test expectedClass = CreateDefaultTest();
            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedClass);

            var testValidationMessage = _fixture.Create<ValidationMessage>();
            _testRepository
                .Setup(x => x.UpdateAsync(expectedClass))
                .ReturnsAsync(testValidationMessage);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

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
