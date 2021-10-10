using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.ValueTypes
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

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}