using System.Diagnostics.CodeAnalysis;

namespace Cifra.Domain.Validation
{
    public class Result
    {
        public bool IsSuccess { get; }
        public ValidationMessage? ValidationMessage { get; }

        protected Result(bool success, ValidationMessage? validationMessage)
        {
            IsSuccess = success;
            ValidationMessage = validationMessage;
        }

        public static Result Fail(ValidationMessage message)
        {
            return new Result(false, message);
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected internal Result(T value, bool success, ValidationMessage? validationMessage) 
            : base (success, validationMessage)
        {
            Value = value;
        }

        public static Result<T> Fail<U>(ValidationMessage validationMessage)
        {
            return new Result<T>(default, false, validationMessage);
        }

        public static Result<T> Ok<U>(T value)
        {
            return new Result<T>(value, true, null);
        }
    }
}
