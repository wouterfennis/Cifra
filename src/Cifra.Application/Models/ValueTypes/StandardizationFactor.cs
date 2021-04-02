using System.Collections.Generic;

namespace Cifra.Application.Models.ValueTypes
{
    /// <summary>
    /// The standardization factor type
    /// </summary>
    public sealed class StandardizationFactor : ValueObject
    {
        private StandardizationFactor(byte value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(byte value)
        {
            // TODO: complete validation
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public byte Value { get; }

        /// <summary>
        /// Creates a StandardizationFactor from a byte
        /// </summary>
        public static StandardizationFactor CreateFromByte(byte value) => new StandardizationFactor(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}