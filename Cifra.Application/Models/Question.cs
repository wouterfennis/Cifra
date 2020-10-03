using System.Collections;
using System.Collections.Generic;

namespace Cifra.Application.Models
{
    public class Question
    {
        public IEnumerable<Name> QuestionNames { get; }

        public QuestionScore MaximalScore { get; }

        public Question(Name questionName, QuestionScore maximalScore)
        {
            QuestionNames = new List<Name> { questionName };
            MaximalScore = maximalScore;
        }
    }
}