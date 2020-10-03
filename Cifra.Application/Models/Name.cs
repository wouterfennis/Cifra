using System;
using System.Collections.Generic;

namespace Cifra.Application.Models
{
    public class Name : ValueObject
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

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}