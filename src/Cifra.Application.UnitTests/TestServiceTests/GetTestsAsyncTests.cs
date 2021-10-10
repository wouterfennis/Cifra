using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
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
        private TestService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testRepository = new Mock<ITestRepository>();
            _testValidator = new Mock<IValidator<CreateTestCommand>>();
            _assignmentValidator = new Mock<IValidator<AddAssignmentCommand>>();
            _sut = new TestService(_testRepository.Object, _testValidator.Object, _assignmentValidator.Object);
        }

        [TestMethod]
        public async Task GetTestsAsync_WithTestsPresent_ReturnsTests()
        {
            var expectedTests = new List<Test>
            {
                CreateDefaultTest(),
                CreateDefaultTest()
            };
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

        private Test CreateDefaultTest()
        {
            return new Test(
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromInteger(5),
                Grade.CreateFromInteger(4),
                1);
        }
    }
}
