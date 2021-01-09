using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests.ClassControllerTests
{
    [TestClass]
    public class AddStudentAsyncTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IValidator<AddStudentRequest>> _studentValidator;
        private ClassController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            var magisterFileReader = new Mock<IMagisterFileReader>();
            var classValidator = new Mock<IValidator<CreateClassRequest>>();
            var magisterClassValidator = new Mock<IValidator<CreateMagisterClassRequest>>();
            _studentValidator = new Mock<IValidator<AddStudentRequest>>();

            _sut = new ClassController(_classRepository.Object,
                magisterFileReader.Object,
                classValidator.Object,
                magisterClassValidator.Object,
                _studentValidator.Object);
        }

        [TestMethod]
        public async Task AddStudentAsync_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddStudentRequest>();
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
            var input = _fixture.Create<AddStudentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddStudentResult result = await _sut.AddStudentAsync(input);

            _classRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddStudentAsync_ClassDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddStudentRequest>();
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
            var input = _fixture.Create<AddStudentRequest>();
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
            var input = _fixture.Create<AddStudentRequest>();
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
