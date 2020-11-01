using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Test entity
    /// </summary>
    public class Test
    {
        public Guid Id { get; }
        public Name Name { get; }
        public List<Question> Questions { get; }
        public StandardizationFactor StandardizationFactor { get; }
        public Grade MinimumGrade { get; }

        /// <summary>
        /// Constructor for a new test
        /// </summary>
        public Test(Name testName, StandardizationFactor standardizationFactor, Grade minimumGrade)
        {
            Id = Guid.NewGuid();
            Name = testName;
            Questions = new List<Question>();
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
        }

        /// <summary>
        /// Constructor for existing tests
        /// </summary>
        public Test(Guid id, 
            Name testName, 
            StandardizationFactor standardizationFactor,
            Grade minimumGrade, 
            List<Question> questions)
        {
            this.Id = id;
            Name = testName;
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
        }

        /// <summary>
        /// Gets the maximal number of question names present of all questions
        /// </summary>
        public int GetMaximalQuestionNamesPerQuestion()
        {
            return Questions.Max(x => x.QuestionNames.Count());
        }

        /// <summary>
        /// Adds a question to the test
        /// </summary>
        public void AddQuestion(Question question)
        {
            if(question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            Questions.Add(question);
        }
    }
}
