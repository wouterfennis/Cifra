using AutoFixture;
using Cifra.FileSystem.FileEntity;

namespace Cifra.TestUtilities.FileSystem
{
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
