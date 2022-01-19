using System.Collections.Generic;

namespace Cifra.Core.Models.ValueTypes
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


        /// <summary>
        /// Implicit converts the <see cref="StandardizationFactor"/> value to <see cref="int"/>.
        /// </summary>
        public static implicit operator int(StandardizationFactor standardizationFactor)
        {
            return standardizationFactor.Value;
        }

        /// <summary>
        /// Implicit converts the <see cref="int"/> value to <see cref="StandardizationFactor"/>.
        /// </summary>
        public static implicit operator StandardizationFactor(int standardizationFactorValue)
        {
            return CreateFromInteger(standardizationFactorValue);
        }

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}