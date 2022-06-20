using AutoFixture;
using Cifra.Core.Models.Test;
using Cifra.Core.Models.ValueTypes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.TestUtilities.Core
{
    [ExcludeFromCodeCoverage] // Part of test project.
    public class TestBuilder
    {
        private readonly Fixture _fixture;
        private Grade _minimumGrade;
        private readonly List<Assignment> _assignments;
        private int _numberOfVersions;

        public TestBuilder()
        {
            _assignments = new List<Assignment>();
            _fixture = new Fixture();
        }

        public TestBuilder WithMinimumGrade(Grade grade)
        {
            _minimumGrade = grade;
            return this;
        }

        public TestBuilder WithValidMinimumGrade()
        {
            _minimumGrade = Grade.CreateFromInteger(1);
            return this;
        }

        public TestBuilder WithNumberOfVersions(int numberOfVersions)
        {
            _numberOfVersions = numberOfVersions;
            return this;
        }

        public TestBuilder WithRandomAssignments()
        {
            for (int i = 0; i < 3; i++)
            {
                var assignment = new AssignmentBuilder()
                    .WithRandomQuestions()
                    .Build();
                _assignments.Add(assignment);
            }
            return this;
        }

        public Test BuildRandomTest()
        {
            Name testName = _fixture.Create<Name>();
            StandardizationFactor standardizationFactor = _fixture.Create<StandardizationFactor>();
            WithRandomAssignments();
            return new Test(
                _fixture.Create<int>(),
                testName,
                standardizationFactor,
                Grade.CreateFromInteger(1),
                _assignments,
                _fixture.Create<int>());
        }

        public Test Build()
        {
            Name testName = _fixture.Create<Name>();
            StandardizationFactor standardizationFactor = _fixture.Create<StandardizationFactor>();
            return new Test(
                _fixture.Create<int>(),
                testName,
                standardizationFactor,
                _minimumGrade,
                _assignments,
                _numberOfVersions);
        }
    }
}
