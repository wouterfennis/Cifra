using AutoFixture;
using Cifra.FileSystem.FileEntity;
using System;
using System.Collections.Generic;

namespace Cifra.TestUtilities.FileSystem
{
    public class TestBuilder
    {
        private readonly Fixture _fixture;
        private byte _minimumGrade;
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
            var standardizationFactor = _fixture.Create<byte>();
            return new Test
            {
                Id = Guid.NewGuid(),
                Name = testName,
                Assignments = _assignments,
                MinimumGrade = _minimumGrade,
                StandardizationFactor = standardizationFactor
            };
        }
    }
}
