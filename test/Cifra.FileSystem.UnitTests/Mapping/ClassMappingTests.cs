using AutoFixture;
using Cifra.Application.Models.Class;
using Cifra.FileSystem.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cifra.FileSystem.UnitTests.Mapping
{
    [TestClass]
    public class ClassMappingTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void MapToFileEntity_InputNull_ThrowsException()
        {
            Class input = null;

            Action action = () => input.MapToFileEntity();

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToModel_InputNull_ThrowsException()
        {
            FileEntity.Class input = null;

            Action action = () => input.MapToModel();

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToFileEntity_WithValidClass_MapsClassProperties()
        {
            Class input = _fixture.Create<Class>();

            FileEntity.Class result = input.MapToFileEntity();

            result.Id.Should().Be(input.Id);
            result.Name.Should().Be(input.Name.Value);
        }

        [TestMethod]
        public void MapToModel_WithValidClass_MapsClassProperties()
        {
            FileEntity.Class input = _fixture.Create<FileEntity.Class>();

            Class result = input.MapToModel();

            result.Id.Should().Be(input.Id);
            result.Name.Value.Should().Be(input.Name);
        }

        [TestMethod]
        public void MapToFileEntity_WithValidClass_MapsStudents()
        {
            Class input = _fixture.Create<Class>();

            FileEntity.Class result = input.MapToFileEntity();

            result.Students.Should().HaveCount(input.Students.Count);
            foreach (var inputStudent in input.Students)
            {
                result.Students.Should().Contain(x => x.FirstName == inputStudent.FirstName.Value &&
                x.Infix == inputStudent.Infix &&
                x.LastName == inputStudent.LastName.Value);
            }
        }

        [TestMethod]
        public void MapToModel_WithValidClass_MapsStudents()
        {
            FileEntity.Class input = _fixture.Create<FileEntity.Class>();

            Class result = input.MapToModel();

            result.Students.Should().HaveCount(input.Students.Count());
            foreach (var inputStudent in input.Students)
            {
                result.Students.Should().Contain(x => x.FirstName.Value == inputStudent.FirstName &&
                x.Infix == inputStudent.Infix &&
                x.LastName.Value == inputStudent.LastName);
            }
        }
    }
}
