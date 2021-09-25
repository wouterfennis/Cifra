using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests.ClassServiceTests
{
    [TestClass]
    public class GetClassesAsyncTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private ClassService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            var magisterFileReader = new Mock<IMagisterFileReader>();
            var classValidator = new Mock<IValidator<CreateClassCommand>>();
            var magisterClassValidator = new Mock<IValidator<CreateMagisterClassCommand>>();
            var studentValidator = new Mock<IValidator<AddStudentCommand>>();
            _sut = new ClassService(_classRepository.Object,
                magisterFileReader.Object,
                classValidator.Object,
                magisterClassValidator.Object,
                studentValidator.Object);
        }

        [TestMethod]
        public async Task GetClassesAsync_WithClassesPresent_ReturnsClasses()
        {
            List<Class> expectedClasses = _fixture.CreateMany<Class>().ToList();
            _classRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedClasses);

            GetAllClassesResult result = await _sut.GetClassesAsync();

            result.Should().NotBeNull();
            result.Classes.Should().NotBeNull();
            result.Classes.Should().BeEquivalentTo(expectedClasses);
        }

        [TestMethod]
        public async Task GetClassesAsync_WithoutClassesPresent_ReturnsEmptyList()
        {
            List<Class> expectedClasses = _fixture.CreateMany<Class>(0).ToList();
            _classRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedClasses);

            GetAllClassesResult result = await _sut.GetClassesAsync();

            result.Should().NotBeNull();
            result.Classes.Should().BeEmpty();
        }
    }
}
