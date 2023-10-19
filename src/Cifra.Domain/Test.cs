using Cifra.Domain.Validation;
using Cifra.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Domain
{
    /// <summary>
    /// The Test entity.
    /// </summary>
    public sealed class Test
    {
        /// <summary>
        /// The Id.
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// The Name.
        /// </summary>
        public Name Name { get; private set; }

        /// <summary>
        /// The number of versions of the test that where made.
        /// </summary>
        public int NumberOfVersions { get; private set; }

        /// <summary>
        /// The Assignments.
        /// </summary>
        public List<Assignment> Assignments { get; private set; }

        /// <summary>
        /// The Standardization Factor.
        /// </summary>
        public StandardizationFactor StandardizationFactor { get; private set; }

        /// <summary>
        /// The Minimum Grade.
        /// </summary>
        public Grade MinimumGrade { get; private set; }

        private Test()
        {
            // Only exists for Entity Framework
        }

        /// <summary>
        /// Constructor for a new Test.
        /// </summary>
        private Test(Name testName, StandardizationFactor standardizationFactor, Grade minimumGrade, int numberOfVersions)
        {
            Name = testName;
            Assignments = new List<Assignment>();
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
            NumberOfVersions = numberOfVersions;
        }

        /// <summary>
        /// Constructor for a existing Test.
        /// </summary>
        private Test(uint id, Name testName, StandardizationFactor standardizationFactor, Grade minimumGrade, int numberOfVersions, List<Assignment> assignments)
        {
            Id = id;
            Name = testName;
            Assignments = new List<Assignment>();
            StandardizationFactor = standardizationFactor;
            MinimumGrade = minimumGrade;
            NumberOfVersions = numberOfVersions;
            Assignments = assignments;
        }

        public static Result<Test> TryCreate(string testName, int standardizationFactor, int minimumGrade, int numberOfVerions)
        {
            Result<Name> testNameResult = Name.CreateFromString(testName);
            Result<StandardizationFactor> standardizationFactorResult = StandardizationFactor.CreateFromInteger(standardizationFactor);
            Result<Grade> minimumGradeResult = Grade.CreateFromInteger(minimumGrade);

            if (!testNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(testName), "Testname is not valid");
                return Result<Test>.Fail<Test>(validationMessage);
            }

            if (!standardizationFactorResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(testName), standardizationFactorResult.ValidationMessage!);
                return Result<Test>.Fail<Test>(validationMessage);
            }

            if (!minimumGradeResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(minimumGrade), minimumGradeResult.ValidationMessage!);
                return Result<Test>.Fail<Test>(validationMessage);
            }

            if (numberOfVerions <= 0)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(numberOfVerions), "There should be at least one version of the test");
                return Result<Test>.Fail<Test>(validationMessage);
            }

            return Result<Test>.Ok<Test>(new Test(testNameResult.Value!, standardizationFactorResult.Value!, minimumGradeResult.Value!, numberOfVerions));
        }

        public static Result<Test> TryCreate(uint id, string testName, int standardizationFactor, int minimumGrade, int numberOfVerions, List<Assignment> assignments)
        {
            Result<Name> testNameResult = Name.CreateFromString(testName);
            Result<StandardizationFactor> standardizationFactorResult = StandardizationFactor.CreateFromInteger(standardizationFactor);
            Result<Grade> minimumGradeResult = Grade.CreateFromInteger(minimumGrade);

            if (!testNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(testName), "Testname is not valid");
                return Result<Test>.Fail<Test>(validationMessage);
            }

            if (!standardizationFactorResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(testName), standardizationFactorResult.ValidationMessage!);
                return Result<Test>.Fail<Test>(validationMessage);
            }

            if (!minimumGradeResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(minimumGrade), minimumGradeResult.ValidationMessage!);
                return Result<Test>.Fail<Test>(validationMessage);
            }

            if (numberOfVerions <= 0)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(numberOfVerions), "There should be at least one version of the test");
                return Result<Test>.Fail<Test>(validationMessage);
            }

            return Result<Test>.Ok<Test>(new Test(id, testNameResult.Value!, standardizationFactorResult.Value!, minimumGradeResult.Value!, numberOfVerions, assignments));
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
        /// Gets the assignment.
        /// </summary>
        public Assignment? GetAssignment(int assignmentId)
        {
            return Assignments.SingleOrDefault(x => x.Id == assignmentId);
        }
    }
}
