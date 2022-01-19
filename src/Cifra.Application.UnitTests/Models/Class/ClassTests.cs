using AutoFixture;
using Cifra.Core.Models.Class;
using Cifra.Core.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application.UnitTests.Models.Class
{
    [TestClass]
    public class ClassTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Constructor_ExistingClassWithStudentsNull_ThrowsException()
        {
            // Arrange
            int id = _fixture.Create<int>();
            Name name = Name.CreateFromString(_fixture.Create<string>());
            List<Student> students = null;

            // Act
            Action action = () => new Core.Models.Class.Class(id, name, students);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddStudent_WithStudent_AddStudentToClass()
        {
            // Arrange
            var sut = new Core.Models.Class.Class(
                _fixture.Create<int>(),
                Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Student>(0).ToList());
            var expectedStudent = _fixture.Create<Student>();

            // Act
            sut.AddStudent(expectedStudent);

            // Assert
            sut.Students.Should().ContainSingle();
            sut.Students.Single().Should().Be(expectedStudent);
        }

        [TestMethod]
        public void AddStudent_WithStudentNull_ThrowsException()
        {
            // Arrange
            var sut = new Core.Models.Class.Class(
                _fixture.Create<int>(),
                Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Student>(0).ToList());

            // Act
            Action action = () => sut.AddStudent(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
