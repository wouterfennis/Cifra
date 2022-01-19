using AutoFixture;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Core.Models.Validation;
using Cifra.Application.Validation;
using Cifra.Database.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Cifra.Database.Schema;
using AutoMapper;

namespace Cifra.Application.UnitTests.ClassServiceTests
{
    [TestClass]
    public class CreateClassAsyncTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IValidator<CreateClassCommand>> _classValidator;
        private Mock<IMapper> _mapper;
        private ClassService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            _classValidator = new Mock<IValidator<CreateClassCommand>>();
            _mapper = new Mock<IMapper>();
            var studentValidator = new Mock<IValidator<AddStudentCommand>>();
            _sut = new ClassService(_classRepository.Object,
                _classValidator.Object,
                studentValidator.Object,
                _mapper.Object);
        }

        [TestMethod]
        public async Task CreateClassAsync_WithValidRequest_CreatesClass()
        {
            // Arrange
            var input = _fixture.Create<CreateClassCommand>();
            var validationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(validationMessages);

            int expectedId = _fixture.Create<int>();
            _classRepository
                .Setup(x => x.CreateAsync(It.Is<Class>(x => x.Name == input.Name)))
                .ReturnsAsync(expectedId);

            // Act
            CreateClassResult result = await _sut.CreateClassAsync(input);

            // Assert
            result.ClassId.Should().Be(expectedId);
            result.ValidationMessages.Should().BeEmpty();
        }

        [TestMethod]
        public async Task CreateClassAsync_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<CreateClassCommand>();
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
            var input = _fixture.Create<CreateClassCommand>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _classValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            await _sut.CreateClassAsync(input);

            _classRepository.VerifyNoOtherCalls();
        }
    }
}
