using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Question entity
    /// </summary>
    public sealed class Question
    {
        public IEnumerable<Name> QuestionNames { get; }

        public QuestionScore MaximalScore { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Question(IEnumerable<Name> questionNames, QuestionScore maximalScore)
        {
            QuestionNames = questionNames ?? throw new ArgumentNullException(nameof(questionNames));
            MaximalScore = maximalScore;
        }
    }
}