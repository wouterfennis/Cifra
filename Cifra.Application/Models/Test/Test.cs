using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Test entity
    /// </summary>
    public sealed class Test
    {
        public Guid Id { get; }
        public Name Name { get; }
        public List<Assignment> Assignments { get; }
        public StandardizationFactor StandardizationFactor { get; }
        public Grade MinimumGrade { get; }

        /// <summary>
        /// Constructor for a new Test
        /// </summary>
        public Test(Name testName, StandardizationFactor standardizationFactor, Grade minimumGrade)
        {
            Id = Guid.NewGuid();
            Name = testName;
            Assignments = new List<Assignment>();
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
            List<Assignment> assignments)
        {
            Id = id;
            Name = testName;
            Assignments = assignments ?? throw new ArgumentNullException(nameof(assignments));
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
        }

        /// <summary>
        /// Gets the maximum points that can be achieved in this test
        /// </summary>
        public decimal GetMaximumPoints()
        {
            return Assignments.Sum(x => x.GetMaximumPoints());
        }

        /// <summary>
        /// Gets the maximum number of question names present of all assignments
        /// </summary>
        public int GetMaximumQuestionNamesPerAssignment()
        {
            if (Assignments.Any())
            {
                return Assignments.Max(x => x.GetMaximumQuestionNamesPerQuestion());
            }
            return 0;
        }

        /// <summary>
        /// Adds a assignment to the test
        /// </summary>
        public void AddAssignment(Assignment assignment)
        {
            if (assignment == null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }
            Assignments.Add(assignment);
        }

        /// <summary>
        /// Gets the assignment
        /// </summary>
        public Assignment GetAssignment(Guid assignmentId)
        {
            return Assignments.SingleOrDefault(x => x.Id == assignmentId);
        }
    }
}
