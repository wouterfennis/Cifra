using Cifra.Domain.Validation;

namespace Cifra.Domain
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public int NumberOfQuestions { get; private set; }

        /// <summary>
        /// // Only exists for Entity Framework.
        /// </summary>
        private Assignment()
        {
            // Only exists for Entity Framework
        }

        /// <summary>
        /// Constructor for new assignment.
        /// </summary>
        private Assignment(int numberOfQuestions)
        {
            NumberOfQuestions = numberOfQuestions;
        }

        /// <summary>
        /// Constructor for existing assignment.
        /// </summary>
        private Assignment(uint id, int numberOfQuestions)
        {
            Id = id;
            NumberOfQuestions = numberOfQuestions;
        }

        public static Result<Assignment> TryCreate(int numberOfQuestions)
        {
            if (numberOfQuestions <= 0)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(numberOfQuestions), "There should be at least one question on a assignment");
                return Result<Assignment>.Fail<Assignment>(validationMessage);
            }

            return Result<Assignment>.Ok<Assignment>(new Assignment(numberOfQuestions));
        }

        public static Result<Assignment> TryCreate(uint? id, int numberOfQuestions)
        {
            uint validId = id ?? 0;

            if (numberOfQuestions <= 0)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(numberOfQuestions), "There should be at least one question on a assignment");
                return Result<Assignment>.Fail<Assignment>(validationMessage);
            }

            return Result<Assignment>.Ok<Assignment>(new Assignment(validId, numberOfQuestions));
        }

        /// <summary>
        /// Update this instance of the assignment with properties from other assignment.
        /// </summary>
        public void UpdateFromOtherAssignment(Assignment otherAssignment)
        {
            NumberOfQuestions = otherAssignment.NumberOfQuestions;
        }
    }
}