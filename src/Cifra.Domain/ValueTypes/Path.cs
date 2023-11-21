using Cifra.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Domain.ValueTypes
{
    /// <summary>
    /// The path type
    /// </summary>
    public sealed class Path
    {
        private Path(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a Path from a string
        /// </summary>
        public static Result<Path> CreateFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<Path>.Fail<Path>(ValidationMessage.Create(nameof(value), "Path cannot be null or empty"));
            }

            return Result<Path>.Ok<Path>(new Path(value));
        }

        /// <summary>
        /// Implicit converts the <see cref="Path"/> value to <see cref="string"/>.
        /// </summary>
        public static implicit operator string(Path path)
        {
            return path.Value;
        }
    }
}
