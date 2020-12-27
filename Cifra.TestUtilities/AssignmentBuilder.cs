using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.TestUtilities
{
    public class AssignmentBuilder
    {
        private readonly Fixture _fixture;
        private readonly List<Question> _questions;

        public AssignmentBuilder()
        {
            _questions = new List<Question>();
            _fixture = new Fixture();
        }

        public AssignmentBuilder WithRandomQuestions()
        {
            for (int i = 0; i < 3; i++)
            {
               var question = new QuestionBuilder()
                    .WithQuestionScore(QuestionScore.CreateFromByte(1))
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
