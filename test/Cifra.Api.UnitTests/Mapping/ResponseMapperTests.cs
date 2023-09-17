﻿using AutoFixture;
using Cifra.Api.Mapping;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Test.Results;
using Cifra.Domain.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Api.UnitTests.Mapping
{
    [TestClass]
    public class ResponseMapperTests
    {
        private Fixture _fixture = default!;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fixture.Customize<Grade>(c => c.FromFactory(() => Grade.CreateFromInteger(1)));
        }

        [TestMethod]
        public void MapToResponse_WithGetAllClassesResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<GetAllClassesResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            foreach (var actualClass in result.Classes)
            {
                var expectedClass = input.Classes.Single(x => x.Name == actualClass.Name);
                expectedClass.Name.Value.Should().Be(actualClass.Name);
                expectedClass.Id.Should().Be(actualClass.Id);
                AssertStudents(actualClass.Students, expectedClass.Students);
            }
        }

        [TestMethod]
        public void MapToResponse_WithGetClassResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<GetClassResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.RetrievedClass.Should().NotBeNull();
            result.RetrievedClass.Name.Should().Be(input.RetrievedClass.Name);  
            result.RetrievedClass.Id.Should().Be(input.RetrievedClass.Id);
            AssertStudents(result.RetrievedClass.Students, input.RetrievedClass.Students);
        }

        [TestMethod]
        public void MapToResponse_WithCreateClassResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<CreateClassResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.ClassId.Should().Be(input.ClassId);
            result.ValidationMessages.Should().BeEquivalentTo(input.ValidationMessages);
        }

        [TestMethod]
        public void MapToResponse_WithUpdateClassResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<UpdateClassResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.ClassId.Should().Be(input.ClassId);
            result.ValidationMessages.Should().BeEquivalentTo(input.ValidationMessages);
        }

        [TestMethod]
        public void MapToResponse_WithGetAllTestsResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<GetAllTestsResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.Tests.Should().NotBeNull();

            foreach (var test in result.Tests)
            {
                var expectedTest = input.Tests.Single(x => x.Name.Value == test.Name);
                expectedTest.Id.Should().Be(test.Id);
                expectedTest.Name.Value.Should().Be(test.Name);
                expectedTest.MinimumGrade.Value.Should().Be(test.MinimumGrade);
                expectedTest.NumberOfVersions.Should().Be(test.NumberOfVersions);
                expectedTest.StandardizationFactor.Value.Should().Be(test.StandardizationFactor);
                AssertAssignments(test.Assignments, expectedTest.Assignments);
            }
        }

        [TestMethod]
        public void MapToResponse_WithGetTestResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<GetTestResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.Test.Should().NotBeNull();
            result.Test.Name.Should().Be(input.Test.Name);
            result.Test.Id.Should().Be(input.Test.Id);
            result.Test.MinimumGrade.Should().Be(input.Test.MinimumGrade);
            result.Test.NumberOfVersions.Should().Be(input.Test.NumberOfVersions);
            result.Test.StandardizationFactor.Should().Be(input.Test.StandardizationFactor);
            result.Test.Assignments.Should().BeEquivalentTo(input.Test.Assignments);
        }

        [TestMethod]
        public void MapToResponse_WithCreateTestResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<CreateTestResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.TestId.Should().Be(input.TestId);
            result.ValidationMessages.Should().BeEquivalentTo(input.ValidationMessages);
        }

        [TestMethod]
        public void MapToResponse_WithUpdateTestResult_MapsToOutput()
        {
            // Arrange
            var input = _fixture.Create<UpdateTestResult>();

            // Act
            var result = input.MapToResponse();

            // Assert
            result.TestId.Should().Be(input.TestId);
            result.ValidationMessages.Should().BeEquivalentTo(input.ValidationMessages);
        }

        private void AssertStudents(IEnumerable<V1.Models.Class.Student> students, IEnumerable<Domain.Student> expectedStudents)
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

        private void AssertAssignments(IEnumerable<V1.Models.Test.Assignment> assignments, IEnumerable<Domain.Assignment> expectedAssignments)
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
