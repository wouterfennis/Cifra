using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.ValueTypes
{
    /// <summary>
    /// The infix type
    /// </summary>
    public sealed class Infix : ValueObject
    {
        private Infix(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string infix)
        {
            // TODO: complete validation
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a Infix from a string
        /// </summary>
        public static Infix CreateFromString(string value) => new Infix(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}