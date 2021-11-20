using AutoFixture;
using AutoMapper;
using Cifra.Application.Models.Test;
using Cifra.Database.Repositories;
using Cifra.TestUtilities.Application;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.Database.UnitTests.Repositories
{
    [TestClass]
    public class TestDatabaseRepositoryTests
    {
        private Mock<IMapper> _mapper;
        private Context _context;
        private TestDatabaseRepository _sut;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new Context(options);

            _sut = new TestDatabaseRepository(_context, _mapper.Object);
        }

        [TestMethod]
        public void Constructor_WithoutDbContext_ThrowsException()
        {
            // Arrange
            Context context = null;

            // Act
            Action action = () => _ = new TestDatabaseRepository(context, _mapper.Object);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WithoutMapper_ThrowsException()
        {
            // Arrange
            IMapper mapper = null;

            // Act
            Action action = () => _ = new TestDatabaseRepository(_context, mapper);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task CreateAsync_WithoutTest_ThrowsException()
        {
            // Arrange
            Test test = null;

            // Act
            Func<Task> func = async () => { await _sut.CreateAsync(test); };

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [TestMethod]
        public async Task CreateAsync_WithTest_SavesTest()
        {
            // Arrange
            var testBuilder = new TestBuilder ();
            Test test = testBuilder.BuildRandomTest();

            var schemaTest = _fixture.Create<Schema.Test>();
            _mapper.Setup(x => x.Map<Schema.Test>(test))
                .Returns(schemaTest);

            // Act
            int result = await _sut.CreateAsync(test);

            // Assert
            result.Should().Be(schemaTest.Id);

            _context.Tests.Should().HaveCount(1);
            var savedTest = _context.Tests.Single();
            savedTest.Should().Be(schemaTest); 
        }

        [TestMethod]
        public async Task GetAsync_WithoutTest_ReturnsNull()
        {
            // Arrange
            int id = _fixture.Create<int>();

            // Act
            Test result = await _sut.GetAsync(id);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetAsync_WithTest_ReturnsTest()
        {
            // Arrange
            Schema.Test test = _fixture.Create<Schema.Test>();
            _context.Tests.Add(test);
            _context.SaveChanges();

            var testBuilder = new TestBuilder();
            var mappedTest = testBuilder.BuildRandomTest();
            _mapper.Setup(x => x.Map<Test>(test))
                .Returns(mappedTest);

            // Act
            Test result = await _sut.GetAsync(test.Id);

            // Assert
            AssertTest(mappedTest, result);
        }

        [TestMethod]
        public async Task GetAllAsync_WithoutTests_ReturnsEmptyList()
        {
            // Arrange
            var expectedList = new List<Test>();
            _mapper.Setup(x => x.Map<List<Test>>(It.Is<List<Schema.Test>>(x => x.Count == 0)))
                .Returns(expectedList);

            // Act
            List<Test> result = await _sut.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetAllAsync_WithMultipleTests_ReturnsAllTests()
        {
            // Arrange
            var tests = _fixture.CreateMany<Schema.Test>().ToList();
            _context.Tests.AddRange(tests);
            _context.SaveChanges();

            var testBuilder = new TestBuilder();
            var mappedTest1 = testBuilder.BuildRandomTest();
            var mappedTest2 = testBuilder.BuildRandomTest();
            var mappedTests = new List<Test> { mappedTest1, mappedTest2 };
            _mapper.Setup(x => x.Map<List<Test>>(tests))
                .Returns(mappedTests);

            // Act
            List<Test> result = await _sut.GetAllAsync();

            // Assert
            AssertTests(mappedTests, result);
        }

        private static void AssertTests(IEnumerable<Test> mappedTests, IEnumerable<Test> result)
        {
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mappedTests);
        }

        private static void AssertTest(Test mappedTest, Test result)
        {
            result.Should().NotBeNull();
            result.MinimumGrade.Should().Be(mappedTest.MinimumGrade);
            result.NumberOfVersions.Should().Be(mappedTest.NumberOfVersions);
            result.Name.Should().Be(mappedTest.Name);
            result.StandardizationFactor.Should().Be(mappedTest.StandardizationFactor);
            AssertAssignments(mappedTest.Assignments, result.Assignments);
        }

        private static void AssertAssignments(IEnumerable <Assignment> expectedAssignments, IEnumerable<Assignment> resultAssignment)
        {
            foreach (var expectedAssignment in expectedAssignments)
            {
                resultAssignment.Should().Contain(x => x.Id == expectedAssignment.Id && x.NumberOfQuestions == expectedAssignment.NumberOfQuestions);
            }
        }
    }
}
