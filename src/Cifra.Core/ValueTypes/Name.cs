using System;
using System.Collections.Generic;

namespace Cifra.Core.Models.ValueTypes
{
    /// <summary>
    /// The name type
    /// </summary>
    public sealed class Name
    {
        private Name(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a Name from a string
        /// </summary>
        public static Name CreateFromString(string value) => new Name(value);

        /// <summary>
        /// Implicit converts the <see cref="Name"/> value to <see cref="string"/>.
        /// </summary>
        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        /// <summary>
        /// Implicit converts the <see cref="string"/> value to <see cref="Name"/>.
        /// </summary>
        public static implicit operator Name(string nameValue)
        {
            return CreateFromString(nameValue);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}