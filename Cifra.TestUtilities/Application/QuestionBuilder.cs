using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using System.Collections.Generic;

namespace Cifra.TestUtilities.Application
{
    public class QuestionBuilder
    {
        private readonly Fixture _fixture;
        private readonly List<Name> _questionNames;
        private QuestionScore _maximumScore;

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

        public QuestionBuilder WithMaximumScore(QuestionScore questionScore)
        {
            _maximumScore = questionScore;
            return this;
        }

        public Question Build()
        {
            return new Question(_questionNames, _maximumScore);
        }
    }
}
