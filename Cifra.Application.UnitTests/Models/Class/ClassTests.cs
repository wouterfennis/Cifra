using AutoFixture;
using Cifra.Application.Models;
using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void Constructor_NewClass_GeneratesId()
        {
            var result = new Application.Models.Class.Class(Name.CreateFromString(_fixture.Create<string>()));

            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void Constructor_ExistingClassWithStudentsNull_ThrowsException()
        {
            Action action = () => new Application.Models.Class.Class(_fixture.Create<Guid>(), Name.CreateFromString(_fixture.Create<string>()), null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddStudent_WithStudent_AddStudentToClass()
        {
            var sut = new Application.Models.Class.Class(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Student>(0).ToList());
            var expectedStudent = new Student(Name.CreateFromString(_fixture.Create<string>()));

            sut.AddStudent(expectedStudent);

            sut.Students.Should().ContainSingle();
            sut.Students.Single().Should().Be(expectedStudent);
        }

        [TestMethod]
        public void AddStudent_WithStudentNull_ThrowsException()
        {
            var sut = new Application.Models.Class.Class(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                _fixture.CreateMany<Student>(0).ToList());

            Action action = () => sut.AddStudent(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
