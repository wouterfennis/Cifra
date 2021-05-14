using System.Threading.Tasks;
using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cifra.Application.UnitTests.ClassServiceTests
{
    [TestClass]
    public class CreateClassAsyncTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IValidator<CreateClassRequest>> _classValidator;
        private ClassService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            var magisterFileReader = new Mock<IMagisterFileReader>();
            _classValidator = new Mock<IValidator<CreateClassRequest>>();
            var magisterClassValidator = new Mock<IValidator<CreateMagisterClassRequest>>();
            var studentValidator = new Mock<IValidator<AddStudentRequest>>();
            _sut = new ClassService(_classRepository.Object,
                magisterFileReader.Object,
                _classValidator.Object,
                magisterClassValidator.Object,
                studentValidator.Object);
        }

        [TestMethod]
        public async Task CreateClassAsync_WithValidRequest_CreatesClass()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            CreateClassResult result = await _sut.CreateClassAsync(input);

            result.ClassId.Should().NotBeEmpty();
            result.ValidationMessages.Should().BeEmpty();

            _classRepository
               .Verify(x => x.CreateAsync(It.Is<Class>(x => x.Name.Value == input.Name)));
        }

        [TestMethod]
        public async Task CreateClassAsync_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateClassResult result = await _sut.CreateClassAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task CreateClassAsync_WithValidationMessages_DoesNotCreateClass()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            await _sut.CreateClassAsync(input);

            _classRepository.VerifyNoOtherCalls();
        }
    }
}
