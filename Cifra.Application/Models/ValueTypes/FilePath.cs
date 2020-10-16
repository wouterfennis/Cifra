using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.Models.ValueTypes
{
    public class FilePath : ValueObject
    {
        private FilePath(string value)
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
        /// Creates a FilePath from a string
        /// </summary>
        public static FilePath CreateFromString(string value) => new FilePath(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}
