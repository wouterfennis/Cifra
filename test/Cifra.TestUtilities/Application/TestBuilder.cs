using AutoFixture;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.TestUtilities.Domain
{
    [ExcludeFromCodeCoverage(Justification = "Part of test project")]
    public class TestBuilder
    {
        private readonly Fixture _fixture;
        private int _minimumGrade;
        private readonly List<Assignment> _assignments;
        private int _numberOfVersions;

        public TestBuilder()
        {
            _assignments = new List<Assignment>();
            _fixture = new Fixture();
        }

        public TestBuilder WithMinimumGrade(int grade)
        {
            _minimumGrade = grade;
            return this;
        }

        public TestBuilder WithValidMinimumGrade()
        {
            _minimumGrade = 1;
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
            return Test.TryCreate(
                _fixture.Create<uint>(),
                testName,
                standardizationFactor,
                1,
                _fixture.Create<int>(),
                _assignments).Value;
        }

        public Test Build()
        {
            Name testName = _fixture.Create<Name>();
            StandardizationFactor standardizationFactor = _fixture.Create<StandardizationFactor>();
            return Test.TryCreate(
                _fixture.Create<uint>(),
                testName,
                standardizationFactor,
                _minimumGrade,
                _numberOfVersions,
                _assignments
                ).Value;
        }
    }
}
