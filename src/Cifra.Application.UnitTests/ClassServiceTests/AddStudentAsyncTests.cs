using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Validation;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests.ClassServiceTests
{
    [TestClass]
    public class AddStudentAsyncTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IValidator<AddStudentCommand>> _studentValidator;
        private ClassService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            var magisterFileReader = new Mock<IMagisterFileReader>();
            var classValidator = new Mock<IValidator<CreateClassCommand>>();
            var magisterClassValidator = new Mock<IValidator<CreateMagisterClassCommand>>();
            _studentValidator = new Mock<IValidator<AddStudentCommand>>();

            _sut = new ClassService(_classRepository.Object,
                magisterFileReader.Object,
                classValidator.Object,
                magisterClassValidator.Object,
                _studentValidator.Object);
        }

        [TestMethod]
        public async Task AddStudentAsync_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddStudentCommand>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddStudentResult result = await _sut.AddStudentAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task AddStudentAsync_WithValidationMessages_DoesNotAddStudent()
        {
            var input = _fixture.Create<AddStudentCommand>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            await _sut.AddStudentAsync(input);

            _classRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddStudentAsync_ClassDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddStudentCommand>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            _classRepository
                .Setup(x => x.GetAsync(input.ClassId))
                .ReturnsAsync((Class)null);


            AddStudentResult result = await _sut.AddStudentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("ClassId");
            validationMessage.Message.Should().Be("No class was found");
        }

        [TestMethod]
        public async Task AddStudentAsync_UpdateFails_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddStudentCommand>();
            var studentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(studentValidationMessages);

            var expectedClass = _fixture.Create<Class>();
            _classRepository
                .Setup(x => x.GetAsync(input.ClassId))
                .ReturnsAsync(expectedClass);

            var classValidationMessage = _fixture.Create<ValidationMessage>();
            _classRepository
                .Setup(x => x.UpdateAsync(expectedClass))
                .ReturnsAsync(classValidationMessage);

            AddStudentResult result = await _sut.AddStudentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            result.ValidationMessages.Should().Contain(classValidationMessage);
        }

        [TestMethod]
        public async Task AddStudentAsync_UpdateSucceeds_ReturnsNoValidationMessages()
        {
            var input = _fixture.Create<AddStudentCommand>();
            var studentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(studentValidationMessages);

            var expectedClass = _fixture.Create<Class>();
            _classRepository
                .Setup(x => x.GetAsync(input.ClassId))
                .ReturnsAsync(expectedClass);

            _classRepository
                .Setup(x => x.UpdateAsync(expectedClass))
                .ReturnsAsync((ValidationMessage)null);

            AddStudentResult result = await _sut.AddStudentAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEmpty();
        }
    }
}
