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
        /// <summary>
        /// The names of the Question
        /// </summary>
        public IEnumerable<Name> QuestionNames { get; }

        /// <summary>
        /// The maximum score
        /// </summary>
        public QuestionScore MaximumScore { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Question(IEnumerable<Name> questionNames, QuestionScore maximalScore)
        {
            QuestionNames = questionNames ?? throw new ArgumentNullException(nameof(questionNames));
            MaximumScore = maximalScore;
        }
    }
}