using AutoFixture;
using Cifra.FileSystem.FileEntity;
using System.Collections.Generic;

namespace Cifra.TestUtilities.FileSystem
{
    public class QuestionBuilder
    {
        private readonly Fixture _fixture;
        private readonly List<string> _questionNames;
        private byte _questionScore;

        public QuestionBuilder()
        {
            _questionNames = new List<string>();
            _fixture = new Fixture();
        }

        public QuestionBuilder WithRandomQuestionNames()
        {
            for (int i = 0; i < 3; i++)
            {
                var question = _fixture.Create<string>();
                _questionNames.Add(question);
            }
            return this;
        }

        public QuestionBuilder WithMaximumScore(byte questionScore)
        {
            _questionScore = questionScore;
            return this;
        }

        public Question Build()
        {
            return new Question{
                QuestionNames = _questionNames, 
                MaximumScore = _questionScore 
            };
        }
    }
}
