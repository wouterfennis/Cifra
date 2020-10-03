using Cifra.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models
{
    public class Test
    {
        public Name TestName { get; }
        public List<Question> Questions { get; }
        public StandardizationFactor StandardizationFactor { get; }
        public Grade MinimumGrade { get; }

        public Test(Name testName, StandardizationFactor standardizationFactor, Grade minimumGrade)
        {
            TestName = testName;
            Questions = new List<Question>();
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }
}
