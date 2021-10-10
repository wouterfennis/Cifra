using System.Collections.Generic;

namespace Cifra.Application.Models.ValueTypes
{
    /// <summary>
    /// The standardization factor type
    /// </summary>
    public sealed class StandardizationFactor : ValueObject
    {
        private StandardizationFactor(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            // TODO: complete validation
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Creates a StandardizationFactor from a int
        /// </summary>
        public static StandardizationFactor CreateFromInteger(int value) => new StandardizationFactor(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}