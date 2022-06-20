using AutoFixture;
using Cifra.Database.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.TestUtilities.Database
{
    [ExcludeFromCodeCoverage] // Part of test project.
    public class AssignmentBuilder
    {
        private int _numberOfQuestions;
        private readonly Fixture _fixture;

        public AssignmentBuilder()
        {
            _fixture = new Fixture();
        }

        public AssignmentBuilder WithRandomQuestions()
        {
            _numberOfQuestions = _fixture.Create<int>();
            return this;
        }

        public Assignment Build()
        {
            return new Assignment
            {
                Id = _fixture.Create<int>(),
                NumberOfQuestions = _numberOfQuestions
            };
        }
    }
}
