namespace Cifra.Application.Validation
{
    /// <summary>
    /// Validation rule for <c>T</c>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRule<in T>
    {
        /// <summary>
        /// Validates model
        /// </summary>
        ValidationMessage Validate(T model);
    }
}
