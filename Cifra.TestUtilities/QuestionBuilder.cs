using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.TestUtilities
{
    public class QuestionBuilder
    {
        private readonly Fixture _fixture;
        private readonly List<Name> _questionNames;
        private QuestionScore _questionScore;

        public QuestionBuilder()
        {
            _questionNames = new List<Name>();
            _fixture = new Fixture();
        }

        public QuestionBuilder WithRandomQuestionNames()
        {
            for (int i = 0; i < 3; i++)
            {
               var question = Name.CreateFromString(_fixture.Create<string>());
                _questionNames.Add(question);
            }
            return this;
        }

        public QuestionBuilder WithQuestionScore(QuestionScore questionScore)
        {
            _questionScore = questionScore;
            return this;
        }

        public Question Build()
        {
            return new Question(_questionNames, _questionScore);
        }
    }
}
