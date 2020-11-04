using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.ValueTypes
{
    public sealed class Grade : ValueObject
    {
        private readonly byte _minimalValue = 0;
        private readonly byte _maximalValue = 10;

        private Grade(byte value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(byte value)
        {
            if (value < _minimalValue || value > _maximalValue)
            {
                throw new ArgumentException($"The value: {value} is not within {_minimalValue} and {_maximalValue}");
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public byte Value { get; }

        /// <summary>
        /// Creates a Grade from a byte
        /// </summary>
        public static Grade CreateFromByte(byte value) => new Grade(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}