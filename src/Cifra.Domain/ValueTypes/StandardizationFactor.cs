using Cifra.Domain.Validation;

namespace Cifra.Domain.ValueTypes
{
    /// <summary>
    /// The standardization factor type
    /// </summary>
    public sealed class StandardizationFactor
    {
        private StandardizationFactor(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Creates a StandardizationFactor from a integer
        /// </summary>
        public static Result<StandardizationFactor> CreateFromInteger(int value)
        {
            return Result<StandardizationFactor>.Ok<StandardizationFactor>(new StandardizationFactor(value));
        }

        /// <summary>
        /// Implicit converts the <see cref="StandardizationFactor"/> value to <see cref="int"/>.
        /// </summary>
        public static implicit operator int(StandardizationFactor standardizationFactor)
        {
            return standardizationFactor.Value;
        }
    }
}