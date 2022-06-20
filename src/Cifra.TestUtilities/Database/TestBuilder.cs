using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Cifra.Database.Schema;

namespace Cifra.TestUtilities.Database
{
    [ExcludeFromCodeCoverage] // Part of test project.
    public class TestBuilder
    {
        private readonly Fixture _fixture;
        private readonly List<Assignment> _assignments;

        public TestBuilder()
        {
            _assignments = new List<Assignment>();
            _fixture = new Fixture();
        }

        public Test BuildRandomTest()
        {
            string testName = _fixture.Create<string>();
            int standardizationFactor = _fixture.Create<int>();
            int numberOfVersions = _fixture.Create<int>();
            List<Assignment> assignments = CreateRandomAssignments();
            return new Test
            {
                Id = _fixture.Create<int>(),
                Name = testName,
                Assignments = assignments,
                MinimumGrade = 1,
                NumberOfVersions = numberOfVersions,
                StandardizationFactor = standardizationFactor
            };
        }

        private List<Assignment> CreateRandomAssignments()
        {
            var assignments = new List<Assignment>();
            for (int i = 0; i < 3; i++)
            {
                var assignment = new AssignmentBuilder()
                    .WithRandomQuestions()
                    .Build();
                _assignments.Add(assignment);
            }
            return assignments;
        }
    }
}
