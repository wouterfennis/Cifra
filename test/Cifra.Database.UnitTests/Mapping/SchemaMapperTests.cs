using AutoFixture;
using Cifra.Database.Schema;
using Cifra.Database.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Database.UnitTests.Mapping
{
    [TestClass]
    public class SchemaMapperTests
    {
        private Fixture _fixture = default!;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void MapToDomain_WithClass_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<Class>();

            // Act
            var result = input.MapToDomain();

            // Assert
            result.Name.Value.Should().Be(input.Name);
            result.Id.Should().Be(input.Id);
            AssertStudents(result.Students, input.Students);
        }

        [TestMethod]
        public void MapToDomain_WithClasses_MapsToOutput()
        {
            // Arrange
            var input = _fixture.CreateMany<Class>();

            // Act
            var result = input.MapToDomain();

            // Assert
            foreach (var actualClass in result)
            {
                var expectedClass = input.Single(x => x.Name == actualClass.Name);
                actualClass.Name.Value.Should().Be(expectedClass.Name);
                actualClass.Id.Should().Be(expectedClass.Id);
                AssertStudents(actualClass.Students, expectedClass.Students);
            }
        }

        public void MapToDomain_WithTest_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<Test>();

            // Act
            var result = input.MapToDomain();

            // Assert
            result.Name.Should().Be(input.Name);
            result.MinimumGrade.Should().Be(input.MinimumGrade);
            result.NumberOfVersions.Should().Be(input.NumberOfVersions);
            result.StandardizationFactor.Should().Be(input.StandardizationFactor);
            AssertAssignments(result.Assignments, input.Assignments);
        }

        public void MapToDomain_WithTests_MapsToOutput()
        {
            // Arrange
            var input = _fixture.CreateMany<Test>();

            // Act
            var result = input.MapToDomain();

            // Assert

            foreach (var actualTest in result)
            {
                var expectedTest = input.Single(x => x.Name == actualTest.Name);
                actualTest.Name.Value.Should().Be(expectedTest.Name);
                actualTest.MinimumGrade.Should().Be(expectedTest.MinimumGrade);
                actualTest.NumberOfVersions.Should().Be(expectedTest.NumberOfVersions);
                actualTest.StandardizationFactor.Should().Be(expectedTest.StandardizationFactor);
                AssertAssignments(actualTest.Assignments, expectedTest.Assignments);
            }
        }

        private void AssertStudents(IEnumerable<Domain.Student> students, IEnumerable<Student> expectedStudents)
        {
            foreach (var student in students)
            {
                var expectedStudent = expectedStudents.Single(x => x.FirstName == student.FirstName);
                student.FirstName.Value.Should().Be(expectedStudent.FirstName);
                student.Infix.Should().Be(expectedStudent.Infix);
                student.LastName.Value.Should().Be(expectedStudent.LastName);
                student.Id.Should().Be(expectedStudent.Id);
            }
        }

        private void AssertAssignments(IEnumerable<Domain.Assignment> assignments, IEnumerable<Assignment> expectedAssignments)
        {
            foreach (var assignment in assignments)
            {
                var expectedTest = expectedAssignments.Single(x => x.Id == assignment.Id);
                assignment.NumberOfQuestions.Should().Be(expectedTest.NumberOfQuestions);
                assignment.Id.Should().Be(expectedTest.Id);
            }
        }
    }
}
