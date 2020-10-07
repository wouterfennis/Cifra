using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cifra.Application.Models.ValueTypes
{
    public class QuestionScore : ValueObject
    {
        private QuestionScore(byte value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(byte value)
        {
            if(value == 0)
            {
                throw new ArgumentException(nameof(value));
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public byte Value { get; }

        /// <summary>
        /// Creates a QuestionScore from a byte
        /// </summary>
        public static QuestionScore CreateFromByte(byte value) => new QuestionScore(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}