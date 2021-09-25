using System;
using System.Collections.Generic;

namespace Cifra.Api.Models.Test
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
        public string Name { get; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public int NumberOfVersions { get; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public List<Assignment> Assignments { get; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        public int StandardizationFactor { get; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public int MinimumGrade { get; }

        /// <summary>
        /// Constructor for a new Test.
        /// </summary>
        public Test(string testName, int standardizationFactor, int minimumGrade, int numberOfVersions)
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
            string testName,
            int standardizationFactor,
            int minimumGrade,
            List<Assignment> assignments,
            int numberOfVersions)
        {
            Id = id;
            Name = testName;
            Assignments = assignments ?? throw new ArgumentNullException(nameof(assignments));
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
            NumberOfVersions = numberOfVersions;
        }
    }
}
