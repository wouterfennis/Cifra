using Cifra.FileSystem.FileEntity;
using System;
using System.Collections.Generic;

namespace Cifra.TestUtilities.FileSystem
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
                     .WithMaximumScore(1)
                     .WithRandomQuestionNames()
                     .Build();
                _questions.Add(question);
            }
            return this;
        }

        public Assignment Build()
        {
            return new Assignment
            {
                Id = Guid.NewGuid(),
                Questions = _questions
            };
        }
    }
}
