using AutoFixture;
using Cifra.Domain;
using Cifra.Database.Mapping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Database.UnitTests.Mapping
{
    [TestClass]
    public class DomainMapperTests
    {
        private Fixture _fixture = default!;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void MapToSchema_WithClass_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<Class>();

            // Act
            var result = input.MapToSchema();

            // Assert
            result.Name.Should().Be(input.Name);
            result.Id.Should().Be(input.Id);
            AssertStudents(result.Students, input.Students);
        }

        public void MapToSchema_WithTest_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<Test>();

            // Act
            var result = input.MapToSchema();

            // Assert
            result.Name.Should().Be(input.Name);
            result.MinimumGrade.Should().Be(input.MinimumGrade);
            result.NumberOfVersions.Should().Be(input.NumberOfVersions);
            result.StandardizationFactor.Should().Be(input.StandardizationFactor);
            AssertAssignments(result.Assignments, input.Assignments);
        }

        private void AssertStudents(IEnumerable<Schema.Student> students, IEnumerable<Student> expectedStudents)
        {
            foreach (var student in students)
            {
                var expectedStudent = expectedStudents.Single(x => x.FirstName == student.FirstName);
                student.FirstName.Should().Be(expectedStudent.FirstName);
                student.Infix.Should().Be(expectedStudent.Infix);
                student.LastName.Should().Be(expectedStudent.LastName);
                student.Id.Should().Be(expectedStudent.Id);
            }
        }

        private void AssertAssignments(IEnumerable<Schema.Assignment> assignments, IEnumerable<Assignment> expectedAssignments)
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
