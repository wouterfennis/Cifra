using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.FileSystem.Mapping;
using Cifra.TestUtilities.FileSystem;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Cifra.FileSystem.UnitTests.Mapping
{
    [TestClass]
    public class TestMappingTests
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
            Test input = null;

            Action action = () => input.MapToFileEntity();

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToModel_InputNull_ThrowsException()
        {
            FileEntity.Test input = null;

            Action action = () => input.MapToModel();

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MapToFileEntity_WithValidTest_MapsTestProperties()
        {
            var input = new TestUtilities.Application.TestBuilder()
                .WithValidMinimumGrade()
                .WithRandomAssignments()
                .Build();

            FileEntity.Test result = input.MapToFileEntity();

            result.Id.Should().Be(input.Id);
            result.Name.Should().Be(input.Name.Value);
            result.MinimumGrade.Should().Be(input.MinimumGrade.Value);
            result.StandardizationFactor.Should().Be(input.StandardizationFactor.Value);
        }

        [TestMethod]
        public void MapToModel_WithValidTest_MapsTestProperties()
        {
            var input = new TestBuilder()
                .WithValidMinimumGrade()
                .WithRandomAssignments()
                .Build();

            Test result = input.MapToModel();

            result.Id.Should().Be(input.Id);
            result.Name.Value.Should().Be(input.Name);
            result.MinimumGrade.Value.Should().Be(input.MinimumGrade);
            result.StandardizationFactor.Value.Should().Be(input.StandardizationFactor);
        }

        [TestMethod]
        public void MapToFileEntity_WithValidTest_MapsAssignments()
        {
            var input = new TestUtilities.Application.TestBuilder()
                .WithValidMinimumGrade()
                .WithRandomAssignments()
                .Build();

            FileEntity.Test result = input.MapToFileEntity();

            result.Assignments.Should().HaveCount(input.Assignments.Count);
            foreach (var assignment in input.Assignments)
            {
                result.Assignments.Should().Contain(x => x.Id == assignment.Id);
            }
        }

        [TestMethod]
        public void MapToModel_WithValidClass_MapsStudents()

        {
            var input = new TestBuilder()
                .WithValidMinimumGrade()
                .WithRandomAssignments()
                .Build();

            Test result = input.MapToModel();

            result.Assignments.Should().HaveCount(input.Assignments.Count());
            foreach (var assignment in input.Assignments)
            {
                result.Assignments.Should().Contain(x => x.Id == assignment.Id);
            }
        }


        [TestMethod]
        public void MapToFileEntity_WithValidTest_MapsQuestions()
        {
            var input = new TestUtilities.Application.TestBuilder()
                .WithValidMinimumGrade()
                .WithRandomAssignments()
                .Build();

            FileEntity.Test result = input.MapToFileEntity();

            result.Assignments.Should().HaveCount(input.Assignments.Count);

            var allQuestions = result.Assignments.SelectMany(x => x.Questions);

            foreach (var assignment in input.Assignments)
            {
                foreach (var question in assignment.Questions)
                {
                    allQuestions.Should().Contain(x => x.MaximumScore == question.MaximumScore.Value);
                }
            }
        }

        [TestMethod]
        public void MapToModel_WithValidClass_MapsQuestions()
        {
            var input = new TestBuilder()
                .WithValidMinimumGrade()
                .WithRandomAssignments()
                .Build();

            Test result = input.MapToModel();

            result.Assignments.Should().HaveCount(input.Assignments.Count());
            foreach (var assignment in input.Assignments)
            {
                result.Assignments.Should().Contain(x => x.Id == assignment.Id);
            }
        }
    }
}
