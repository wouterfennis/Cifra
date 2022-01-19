using AutoFixture;
using AutoMapper;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Validation;
using Cifra.Database.Repositories;
using Cifra.Database.Schema;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Application.UnitTests.TestServiceTests
{
    [TestClass]
    public class GetTestsAsyncTests
    {
        private Fixture _fixture;
        private Mock<ITestRepository> _testRepository;
        private Mock<IValidator<CreateTestCommand>> _testValidator;
        private Mock<IValidator<AddAssignmentCommand>> _assignmentValidator;
        private Mock<IMapper> _mapper;

        private TestService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testRepository = new Mock<ITestRepository>();
            _testValidator = new Mock<IValidator<CreateTestCommand>>();
            _assignmentValidator = new Mock<IValidator<AddAssignmentCommand>>();
            _mapper = new Mock<IMapper>();
            _sut = new TestService(_testRepository.Object, _testValidator.Object, _assignmentValidator.Object, _mapper.Object);
        }

        [TestMethod]
        public async Task GetTestsAsync_WithTestsPresent_ReturnsTests()
        {
            var expectedTests = _fixture.CreateMany<Test>().ToList();
            _testRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedTests);

            GetAllTestsResult result = await _sut.GetTestsAsync();

            result.Should().NotBeNull();
            result.Tests.Should().BeEquivalentTo(expectedTests);
        }

        [TestMethod]
        public async Task GetTestsAsync_WithoutTestsPresent_ReturnsEmptyList()
        {
            var expectedTests = new List<Test>();
            _testRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedTests);

            GetAllTestsResult result = await _sut.GetTestsAsync();

            result.Should().NotBeNull();
            result.Tests.Should().BeEmpty();
        }
    }
}
