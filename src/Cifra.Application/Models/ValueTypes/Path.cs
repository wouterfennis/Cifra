using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.ValueTypes
{
    /// <summary>
    /// The path type
    /// </summary>
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

        /// <summary>
        /// Implicit converts the <see cref="Path"/> value to <see cref="string"/>.
        /// </summary>
        public static implicit operator string(Path path)
        {
            return path.Value;
        }

        /// <summary>
        /// Implicit converts the <see cref="string"/> value to <see cref="Path"/>.
        /// </summary>
        public static implicit operator Path(string pathValue)
        {
            return CreateFromString(pathValue);
        }

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}
