using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.ValueTypes
{
    public sealed class Path : ValueObject
    {
        private Path(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            // TODO: complete validation
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a Path from a string
        /// </summary>
        public static Path CreateFromString(string value) => new Path(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}
