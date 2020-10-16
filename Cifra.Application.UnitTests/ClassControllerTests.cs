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
        public void CreateClass_WithValidRequest_CreatesClass()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            var expectedClassId = _fixture.Create<Guid>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            _classRepository
                .Setup(x => x.Create(It.Is<Class>(x => x.Name.Value == input.Name)))
                .Returns(expectedClassId);

            CreateClassResult result = _sut.CreateClass(input);

            result.ClassId.Should().Be(expectedClassId);
            result.ValidationMessages.Should().BeEmpty();
        }

        [TestMethod]
        public void CreateClass_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateClassResult result = _sut.CreateClass(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public void CreateClass_WithValidationMessages_DoesNotCreateClass()
        {
            var input = _fixture.Create<CreateClassRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            CreateClassResult result = _sut.CreateClass(input);

            _classRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void AddStudent_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddStudentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddStudentResult result = _sut.AddStudent(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public void AddStudent_WithValidationMessages_DoesNotAddStudent()
        {
            var input = _fixture.Create<AddStudentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddStudentResult result = _sut.AddStudent(input);

            _classRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void AddStudent_ClassDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddStudentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            _classRepository
                .Setup(x => x.Get(input.ClassId))
                .Returns((Class)null);

            AddStudentResult result = _sut.AddStudent(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("ClassId");
            validationMessage.Message.Should().Be("No class was found");
        }

        [TestMethod]
        public void AddStudent_UpdateFails_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddStudentRequest>();
            var studentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _studentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(studentValidationMessages);

            var expectedClass = _fixture.Create<Class>();
            _classRepository
                .Setup(x => x.Get(input.ClassId))
                .Returns(expectedClass);

            var classValidationMessage = _fixture.Create<ValidationMessage>();
            _classRepository
                .Setup(x => x.Update(expectedClass))
                .Returns(classValidationMessage);

            AddStudentResult result = _sut.AddStudent(input);

            result.ValidationMessages.Should().ContainSingle();
            result.ValidationMessages.Should().Contain(classValidationMessage);
        }
    }
}
