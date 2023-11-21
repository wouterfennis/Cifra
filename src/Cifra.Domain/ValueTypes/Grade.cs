using Cifra.Domain.Validation;

namespace Cifra.Domain.ValueTypes
{
    /// <summary>
    /// The grade type
    /// </summary>
    public sealed class Grade
    {
        private const int _minimalValue = 1;
        private const int _maximalValue = 10;

        private Grade(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Creates a Grade from a integer
        /// </summary>
        public static Result<Grade> CreateFromInteger(int value)
        {
            if (value < _minimalValue || value > _maximalValue)
            {
                return Result<Grade>.Fail<Grade>(ValidationMessage.Create(nameof(value), $"Minimum grade must be from {_minimalValue} to {_maximalValue}"));
            }

            return Result<Grade>.Ok<Grade>(new Grade(value));
        }

        /// <summary>
        /// Implicit converts the <see cref="Grade"/> value to <see cref="int"/>.
        /// </summary>
        public static implicit operator int(Grade grade)
        {
            return grade.Value;
        }
    }
}