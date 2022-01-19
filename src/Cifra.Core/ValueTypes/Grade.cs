using System;
using System.Collections.Generic;

namespace Cifra.Core.Models.ValueTypes
{
    /// <summary>
    /// The grade type
    /// </summary>
    public sealed class Grade : ValueObject
    {
        private readonly int _minimalValue = 0;
        private readonly int _maximalValue = 10;

        private Grade(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < _minimalValue || value > _maximalValue)
            {
                throw new ArgumentException($"The value: {value} is not within {_minimalValue} and {_maximalValue}");
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Creates a Grade from a integer.
        /// </summary>
        public static Grade CreateFromInteger(int value) => new Grade(value);

        /// <summary>
        /// Implicit converts the <see cref="Grade"/> value to <see cref="int"/>.
        /// </summary>
        public static implicit operator int(Grade grade)
        {
            return grade.Value;
        }

        /// <summary>
        /// Implicit converts the <see cref="int"/> value to <see cref="Grade"/>.
        /// </summary>
        public static implicit operator Grade(int gradeValue)
        {
            return CreateFromInteger(gradeValue);
        }

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}