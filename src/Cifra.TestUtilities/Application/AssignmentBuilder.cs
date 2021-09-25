using System;
using Cifra.Application.Models.Test;

namespace Cifra.TestUtilities.Application
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
            return new Assignment(Guid.NewGuid(), _numberOfQuestions);
        }
    }
}
