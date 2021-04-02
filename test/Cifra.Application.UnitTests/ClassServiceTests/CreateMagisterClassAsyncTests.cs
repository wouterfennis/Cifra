using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Magister;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests.ClassServiceTests
{
    [TestClass]
    public class CreateMagisterClassAsyncTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IMagisterFileReader> _magisterFileReader;
        private Mock<IValidator<CreateMagisterClassRequest>> _magisterClassValidator;
        private ClassService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            _magisterFileReader = new Mock<IMagisterFileReader>();
            var classValidator = new Mock<IValidator<CreateClassRequest>>();
            _magisterClassValidator = new Mock<IValidator<CreateMagisterClassRequest>>();
            var studentValidator = new Mock<IValidator<AddStudentRequest>>();
            _sut = new ClassService(_classRepository.Object,
                _magisterFileReader.Object,
                classValidator.Object,
                _magisterClassValidator.Object,
                studentValidator.Object);
        }

        [TestMethod]
        public async Task CreateMagisterClassAsync_WithValidRequest_CreatesClass()
        {
            // Arrange
            var input = _fixture.Create<CreateMagisterClassRequest>();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _magisterClassValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            var expectedMagisterClass = _fixture.Create<MagisterClass>();
            _magisterFileReader
                .Setup(x => x.ReadClass(It.Is<Path>(p => p.Value == input.MagisterFileLocation)))
                .Returns(expectedMagisterClass);

            // Act
            CreateMagisterClassResult result = await _sut.CreateMagisterClassAsync(input);

            // Assert
            result.ClassId.Should().NotBeEmpty();
            result.ValidationMessages.Should().BeEmpty();

            _classRepository
               .Verify(x => x.CreateAsync(It.Is<Class>(x => x.Name.Value == expectedMagisterClass.Name)));
        }

        [TestMethod]
        public async Task CreateMagisterClassAsync_WithValidRequest_CreatesStudents()
        {
            // Arrange
            var input = _fixture.Create<CreateMagisterClassRequest>();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _magisterClassValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            var expectedMagisterClass = _fixture.Create<MagisterClass>();
            _magisterFileReader
                .Setup(x => x.ReadClass(It.Is<Path>(p => p.Value == input.MagisterFileLocation)))
                .Returns(expectedMagisterClass);
            Class actualClass = null;
            _classRepository
               .Setup(x => x.CreateAsync(It.Is<Class>(x => x.Name.Value == expectedMagisterClass.Name)))
               .Callback((Class @class) =>
               {
                   actualClass = @class;
               });

            // Act
            CreateMagisterClassResult result = await _sut.CreateMagisterClassAsync(input);

            // Assert
            actualClass.Should().NotBeNull();
            actualClass.Students.Should().HaveCount(expectedMagisterClass.Students.Count());
            foreach (var actualStudent in actualClass.Students)
            {
                expectedMagisterClass.Students.Should().Contain(x => x.FirstName == actualStudent.FirstName.Value &&
                x.Infix == actualStudent.Infix &&
                x.LastName == actualStudent.LastName.Value);
            }
        }

        [TestMethod]
        public async Task CreateMagisterClassAsync_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<CreateMagisterClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _magisterClassValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateMagisterClassResult result = await _sut.CreateMagisterClassAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task CreateMagisterClassAsync_WithValidationMessages_DoesNotCreateClass()
        {
            var input = _fixture.Create<CreateMagisterClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _magisterClassValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateMagisterClassResult result = await _sut.CreateMagisterClassAsync(input);

            _classRepository.VerifyNoOtherCalls();
        }
    }
}
