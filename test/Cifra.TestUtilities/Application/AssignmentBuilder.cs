using AutoFixture;
using Cifra.Domain;

namespace Cifra.TestUtilities.Domain
{
    public class AssignmentBuilder
    {
        private uint _id;
        private int _numberOfQuestions;
        private readonly Fixture _fixture;

        public AssignmentBuilder()
        {
            _fixture = new Fixture();
        }

        public AssignmentBuilder WithRandomQuestions()
        {
            _id = _fixture.Create<uint>();
            _numberOfQuestions = 3;
            return this;
        }

        public Assignment Build()
        {
            return Assignment.TryCreate(_id, _numberOfQuestions).Value;
        }
    }
}
