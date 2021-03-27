using System;
using Cifra.Application.Models.Test;
using Cifra.FileSystem.Mapping;
using Cifra.TestUtilities.FileSystem;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.FileSystem.UnitTests.Mapping
{
    [TestClass]
    public class TestMappingTests
    {
        [TestMethod]
        public void MapToFileEntity_InputNull_ThrowsException()
        {
            // Arrange
            Test input = null;

            // Act
            Action action = () => input.MapToFileEntity();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToModel_InputNull_ThrowsException()
        {
            // Arrange
            FileEntity.Test input = null;

            // Act
            Action action = () => input.MapToModel();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToFileEntity_WithValidTest_MapsTestProperties()
        {
            // Arrange
            var input = new TestUtilities.Application.TestBuilder()
                .WithValidMinimumGrade()
                .WithNumberOfVersions(1)
                .WithRandomAssignments()
                .Build();

            // Act
            FileEntity.Test result = input.MapToFileEntity();

            // Assert
            result.Id.Should().Be(input.Id);
            result.Name.Should().Be(input.Name.Value);
            result.MinimumGrade.Should().Be(input.MinimumGrade.Value);
            result.StandardizationFactor.Should().Be(input.StandardizationFactor.Value);
            result.NumberOfVersions.Should().Be(input.NumberOfVersions);
        }

        [TestMethod]
        public void MapToModel_WithValidTest_MapsTestProperties()
        {
            // Arrange
            var input = new TestBuilder()
                .WithValidMinimumGrade()
                .WithNumberOfVersions(1)
                .WithRandomAssignments()
                .Build();

            // Act
            Test result = input.MapToModel();

            // Assert
            result.Id.Should().Be(input.Id);
            result.Name.Value.Should().Be(input.Name);
            result.MinimumGrade.Value.Should().Be(input.MinimumGrade);
            result.StandardizationFactor.Value.Should().Be(input.StandardizationFactor);
            result.NumberOfVersions.Should().Be(input.NumberOfVersions);

        }

        [TestMethod]
        public void MapToFileEntity_WithValidTest_MapsAssignments()
        {
            // Arrange
            var input = new TestUtilities.Application.TestBuilder()
                .WithValidMinimumGrade()
                .WithNumberOfVersions(1)
                .WithRandomAssignments()
                .Build();

            // Act
            FileEntity.Test result = input.MapToFileEntity();

            // Assert
            result.Assignments.Should().HaveCount(input.Assignments.Count);
            foreach (var assignment in input.Assignments)
            {
                result.Assignments.Should().Contain(x => x.Id == assignment.Id);
            }
        }
    }
}
