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
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests
{
    [TestClass]
    public class ClassControllerTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IValidator<CreateClassRequest>> _classValidator;
        private Mock<IValidator<AddStudentRequest>> _studentValidator;
        private ClassController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            _classValidator = new Mock<IValidator<CreateClassRequest>>();
            _studentValidator = new Mock<IValidator<AddStudentRequest>>();
            _sut = new ClassController(_classRepository.Object, _classValidator.Object, _studentValidator.Object);
        }

        [TestMethod]
        public async Task CreateClass_WithValidRequest_CreatesClassAsync()
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
        public async Task CreateClass_WithValidationMessages_ReturnsValidationMessagesAsync()
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
        public async Task CreateClass_WithValidationMessages_DoesNotCreateClassAsync()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateClassResult result = await _sut.CreateClassAsync(input);

            _classRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddStudent_WithValidationMessages_ReturnsValidationMessagesAsync()
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
        public async Task AddStudent_WithValidationMessages_DoesNotAddStudentAsync()
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
        public async Task AddStudent_ClassDoesNotExists_ReturnsValidationMessageAsync()
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
        public async Task AddStudent_UpdateFails_ReturnsValidationMessageAsync()
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
    }
}
