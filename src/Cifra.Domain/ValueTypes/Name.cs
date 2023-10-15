using Cifra.Domain.Validation;

namespace Cifra.Domain.ValueTypes
{
    /// <summary>
    /// The name type
    /// </summary>
    public sealed class Name
    {
        private Name(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a Name from a string
        /// </summary>
        public static Result<Name> CreateFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<Name>.Ok<Name>(new Name(value));
            }

            return Result<Name>.Fail<Name>(ValidationMessage.Create(nameof(value), "Name cannot be null or empty"));
        }

        /// <summary>
        /// Implicit converts the <see cref="Name"/> value to <see cref="string"/>.
        /// </summary>
        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}