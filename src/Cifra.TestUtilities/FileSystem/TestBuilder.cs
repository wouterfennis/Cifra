using System;
using System.Collections.Generic;
using AutoFixture;
using Cifra.FileSystem.FileEntity;

namespace Cifra.TestUtilities.FileSystem
{
    public class TestBuilder
    {
        private readonly Fixture _fixture;
        private int _minimumGrade;
        private int _numberOfVersions;
        private readonly List<Assignment> _assignments;

        public TestBuilder()
        {
            _assignments = new List<Assignment>();
            _fixture = new Fixture();
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

        public Test Build()
        {
            string testName = _fixture.Create<string>();
            var standardizationFactor = _fixture.Create<int>();
            return new Test
            {
                Id = _fixture.Create<int>(),
                Name = testName,
                Assignments = _assignments,
                MinimumGrade = _minimumGrade,
                NumberOfVersions = _numberOfVersions,
                StandardizationFactor = standardizationFactor
            };
        }
    }
}
