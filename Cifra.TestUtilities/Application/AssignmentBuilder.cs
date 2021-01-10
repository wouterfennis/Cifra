using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;

namespace Cifra.TestUtilities.Application
{
    public class AssignmentBuilder
    {
        private readonly List<Question> _questions;

        public AssignmentBuilder()
        {
            _questions = new List<Question>();
        }

        public AssignmentBuilder WithRandomQuestions()
        {
            for (int i = 0; i < 3; i++)
            {
                var question = new QuestionBuilder()
                     .WithMaximumScore(QuestionScore.CreateFromByte(1))
                     .WithRandomQuestionNames()
                     .Build();
                _questions.Add(question);
            }
            return this;
        }

        public Assignment Build()
        {
            return new Assignment(Guid.NewGuid(), _questions);
        }
    }
}
