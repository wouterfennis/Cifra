using AutoFixture;
using AutoMapper;
using Cifra.Application.Models.Class;
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
    public class ClassDatabaseRepositoryTests
    {
        private Mock<IMapper> _mapper;
        private Context _context;
        private ClassDatabaseRepository _sut;
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

            _sut = new ClassDatabaseRepository(_context, _mapper.Object);
        }

        [TestMethod]
        public void Constructor_WithoutDbContext_ThrowsException()
        {
            // Arrange
            Context context = null;

            // Act
            Action action = () => _ = new ClassDatabaseRepository(context, _mapper.Object);

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
            var classBuilder = new ClassBuilder();
            Class newClass = classBuilder.BuildRandomClass();

            var schemaClass = _fixture.Create<Schema.Class>();
            _mapper.Setup(x => x.Map<Schema.Class>(newClass))
                .Returns(schemaClass);

            // Act
            int result = await _sut.CreateAsync(newClass);

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

            var classBuilder = new ClassBuilder();
            var mappedClass = classBuilder.BuildRandomClass();
            _mapper.Setup(x => x.Map<Class>(savedClass))
                .Returns(mappedClass);

            // Act
            Class result = await _sut.GetAsync(savedClass.Id);

            // Assert
            AssertClass(mappedClass, result);
        }

        [TestMethod]
        public async Task GetAllAsync_WithoutClasses_ReturnsEmptyList()
        {
            // Arrange
            var expectedList = new List<Class>();
            _mapper.Setup(x => x.Map<List<Class>>(It.Is<List<Schema.Class>>(x => x.Count == 0)))
                .Returns(expectedList);

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

            var classBuilder = new ClassBuilder();
            Class mappedClass1 = classBuilder.BuildRandomClass();
            Class mappedClass2 = classBuilder.BuildRandomClass();
            var mappedClasses = new List<Class> { mappedClass1, mappedClass2 };
            _mapper.Setup(x => x.Map<List<Class>>(classes))
                .Returns(mappedClasses);

            // Act
            List<Class> result = await _sut.GetAllAsync();

            // Assert
            AssertClasses(mappedClasses, result);
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
