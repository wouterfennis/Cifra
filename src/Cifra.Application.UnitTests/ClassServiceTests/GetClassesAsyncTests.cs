using AutoFixture;
using AutoMapper;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Validation;
using Cifra.Database.Repositories;
using Cifra.Database.Schema;
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
        private Mock<IMapper> _mapper;
        private ClassService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            _mapper = new Mock<IMapper>();
            var classValidator = new Mock<IValidator<CreateClassCommand>>();
            var studentValidator = new Mock<IValidator<AddStudentCommand>>();
            _sut = new ClassService(_classRepository.Object,
                classValidator.Object,
                studentValidator.Object,
                _mapper.Object);
        }

        [TestMethod]
        public async Task GetClassesAsync_WithClassesPresent_ReturnsClasses()
        {
            List<Class> expectedClasses = _fixture.CreateMany<Class>().ToList();
            _classRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedClasses);

            var mappedClasses = new List<Core.Models.Class.Class> { new Core.Models.Class.Class(null) };
            _mapper.Setup(x => x.Map<List<Core.Models.Class.Class>>(expectedClasses))
                .Returns(mappedClasses);

            GetAllClassesResult result = await _sut.GetClassesAsync();

            result.Should().NotBeNull();
            result.Classes.Should().NotBeNull();
            result.Classes.Should().BeEquivalentTo(mappedClasses);
        }
    }
}
