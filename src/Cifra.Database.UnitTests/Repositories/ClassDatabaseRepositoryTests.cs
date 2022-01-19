using AutoFixture;
using AutoMapper;
using Cifra.Database.Repositories;
using Cifra.Database.Schema;
using Cifra.TestUtilities.Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Database.UnitTests.Repositories
{
    [TestClass]
    public class ClassDatabaseRepositoryTests
    {
        private Context _context;
        private ClassDatabaseRepository _sut;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new Context(options);

            _sut = new ClassDatabaseRepository(_context);
        }

        [TestMethod]
        public void Constructor_WithoutDbContext_ThrowsException()
        {
            // Arrange
            Context context = null;

            // Act
            Action action = () => _ = new ClassDatabaseRepository(context);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task CreateAsync_WithoutClass_ThrowsException()
        {
            // Arrange
            Class nullClass = null;

            // Act
            Func<Task> func = async () => { await _sut.CreateAsync(nullClass); };

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        [TestMethod]
        public async Task CreateAsync_WithClass_SavesTest()
        {
            // Arrange
            var schemaClass = _fixture.Create<Class>();

            // Act
            int result = await _sut.CreateAsync(schemaClass);

            // Assert
            result.Should().Be(schemaClass.Id);

            _context.Classes.Should().HaveCount(1);
            var savedClass = _context.Classes.Single();
            savedClass.Should().Be(schemaClass);
        }

        [TestMethod]
        public async Task GetAsync_WithoutClass_ReturnsNull()
        {
            // Arrange
            int id = _fixture.Create<int>();

            // Act
            Class result = await _sut.GetAsync(id);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetAsync_WithClass_ReturnsClass()
        {
            // Arrange
            Schema.Class savedClass = _fixture.Create<Schema.Class>();
            _context.Classes.Add(savedClass);
            _context.SaveChanges();

            // Act
            Class result = await _sut.GetAsync(savedClass.Id);

            // Assert
            AssertClass(savedClass, result);
        }

        [TestMethod]
        public async Task GetAllAsync_WithoutClasses_ReturnsEmptyList()
        {
            // Arrange

            // Act
            List<Class> result = await _sut.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetAllAsync_WithMultipleTests_ReturnsAllTests()
        {
            // Arrange
            var classes = _fixture.CreateMany<Schema.Class>().ToList();
            _context.Classes.AddRange(classes);
            _context.SaveChanges();

            // Act
            List<Class> result = await _sut.GetAllAsync();

            // Assert
            AssertClasses(classes, result);
        }

        private static void AssertClasses(IEnumerable<Class> mappedClasses, IEnumerable<Class> result)
        {
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mappedClasses);
        }

        private static void AssertClass(Class mappedTest, Class result)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(mappedTest.Id);
            result.Name.Should().Be(mappedTest.Name);
            AssertStudents(mappedTest.Students, result.Students);
        }

        private static void AssertStudents(IEnumerable<Student> expectedStudents, IEnumerable<Student> resultStudents)
        {
            foreach (var expectedstudent in expectedStudents)
            {
                resultStudents.Should().Contain(x => x.FirstName == expectedstudent.FirstName && 
                x.Infix == expectedstudent.Infix &&
                x.LastName == expectedstudent.LastName);
            }
        }
    }
}
