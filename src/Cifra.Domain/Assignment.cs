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
        public int Id { get; private set; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public int NumberOfQuestions { get; private set; }

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

        public static Result<Assignment> TryCreate(int numberOfQuestions)
        {
            if (numberOfQuestions <= 0)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(numberOfQuestions), "There should be at least one question on a assignment");
                return Result<Assignment>.Fail<Assignment>(validationMessage);
            }

            return Result<Assignment>.Ok<Assignment>(new Assignment(numberOfQuestions));
        }
    }
}