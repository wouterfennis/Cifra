using Cifra.Domain;

namespace Cifra.TestUtilities.Domain
{
    public class AssignmentBuilder
    {
        private int _numberOfQuestions;

        public AssignmentBuilder WithRandomQuestions()
        {
            _numberOfQuestions = 3;
            return this;
        }

        public Assignment Build()
        {
            return new Assignment(1234, _numberOfQuestions);
        }
    }
}
