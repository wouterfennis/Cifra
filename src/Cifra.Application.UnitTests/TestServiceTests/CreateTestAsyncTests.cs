using AutoFixture;
using AutoMapper;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Validation;
using Cifra.Core.Models.Validation;
using Cifra.Database.Repositories;
using Cifra.Database.Schema;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests.TestServiceTests
{
    [TestClass]
    public class CreateTestAsyncTests
    {
        private Fixture _fixture;
        private Mock<ITestRepository> _testRepository;
        private Mock<IValidator<CreateTestCommand>> _testValidator;
        private Mock<IValidator<AddAssignmentCommand>> _assignmentValidator;
        private Mock<IMapper> _mapper;
        private TestService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testRepository = new Mock<ITestRepository>();
            _testValidator = new Mock<IValidator<CreateTestCommand>>();
            _assignmentValidator = new Mock<IValidator<AddAssignmentCommand>>();
            _mapper = new Mock<IMapper>();
            _sut = new TestService(_testRepository.Object, 
                _testValidator.Object, 
                _assignmentValidator.Object,
                _mapper.Object);
        }

        [TestMethod]
        public async Task CreateTestAsync_WithValidRequest_CreatesClass()
        {
            CreateTestCommand input = CreateDefaultTestRequest();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            CreateTestResult result = await _sut.CreateTestAsync(input);

            result.TestId.Should().Be(default);
            result.ValidationMessages.Should().BeEmpty();

            _testRepository
                .Verify(x => x.CreateAsync(It.Is<Test>(x => x.Name == input.Name)));
        }

        [TestMethod]
        public async Task CreateTestAsync_WithValidationMessages_ReturnsValidationMessages()
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
        public async Task CreateTestAsync_WithValidationMessages_DoesNotCreateClass()
        {
            var input = CreateDefaultTestRequest();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _testValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            await _sut.CreateTestAsync(input);

            _testRepository.VerifyNoOtherCalls();
        }

        private CreateTestCommand CreateDefaultTestRequest()
        {
            return _fixture.Build<CreateTestCommand>()
                .With(x => x.MinimumGrade, 5)
                .With(x => x.StandardizationFactor, 5)
                .Create();
        }
    }
}
