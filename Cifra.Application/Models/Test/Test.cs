using System;
using System.Collections.Generic;
using System.Linq;
using Cifra.Application.Models.ValueTypes;

namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Test entity.
    /// </summary>
    public sealed class Test
    {
        /// <summary>
        /// The Id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The Name.
        /// </summary>
        public Name Name { get; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public int NumberOfVersions { get; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public List<Assignment> Assignments { get; }

        /// <summary>
        /// The bonus assignment.
        /// </summary>
        public Assignment BonusAssignment { get; private set; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        public StandardizationFactor StandardizationFactor { get; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public Grade MinimumGrade { get; }

        /// <summary>
        /// Constructor for a new Test.
        /// </summary>
        public Test(Name testName, StandardizationFactor standardizationFactor, Grade minimumGrade, int numberOfVersions)
        {
            Id = Guid.NewGuid();
            Name = testName;
            Assignments = new List<Assignment>();
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
            NumberOfVersions = numberOfVersions;
        }

        /// <summary>
        /// Constructor for existing tests.
        /// </summary>
        public Test(Guid id,
            Name testName,
            StandardizationFactor standardizationFactor,
            Grade minimumGrade,
            List<Assignment> assignments,
            Assignment bonusAssignment,
            int numberOfVersions)
        {
            Id = id;
            Name = testName;
            Assignments = assignments ?? throw new ArgumentNullException(nameof(assignments));
            BonusAssignment = bonusAssignment;
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
            NumberOfVersions = numberOfVersions;
        }

        /// <summary>
        /// Adds a assignment to the test.
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
        /// Adds a assignment to the test.
        /// </summary>
        public void SetBonusAssignment(Assignment assignment)
        {
            if (assignment == null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }
            BonusAssignment = assignment;
        }

        /// <summary>
        /// Gets the assignment.
        /// </summary>
        public Assignment GetAssignment(Guid assignmentId)
        {
            return Assignments.SingleOrDefault(x => x.Id == assignmentId);
        }
    }
}
