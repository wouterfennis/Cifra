using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Assignment entity
    /// </summary>
    public sealed class Assignment
    {
        public Guid  Id { get; }
        public Name Name { get; }
        public List<Question> Questions { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Assignment(Name name, List<Question> questions)
        {
            Name = name;
            Questions = questions;
        }

        /// <summary>
        /// Constructor for a new Assignment
        /// </summary>
        public Assignment(Name name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Questions = new List<Question>();
        }

        /// <summary>
        /// Gets the maximum points that can be achieved in this assignment
        /// </summary>
        public decimal GetMaximumPoints()
        {
            return Questions.Sum(x => x.MaximalScore.Value);
        }

        /// <summary>
        /// Gets the maximum number of question names present of all assignments
        /// </summary>
        public int GetMaximumQuestionNamesPerQuestion()
        {
            if (Questions.Any())
            {
                return Questions.Max(x => x.QuestionNames.Count());
            }
            return 0;
        }

        /// <summary>
        /// Adds a question to the assignment
        /// </summary>
        public void AddQuestion(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            Questions.Add(question);
        }
    }
}