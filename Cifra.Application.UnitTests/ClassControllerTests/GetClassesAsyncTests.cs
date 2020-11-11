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
using System.Collections.Generic;

namespace Cifra.Application.UnitTests.ClassControllerTests
{
    [TestClass]
    public class GetClassesAsyncTests
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
