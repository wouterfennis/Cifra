using Cifra.Domain.Validation;
using Cifra.Domain.ValueTypes;

namespace Cifra.Domain
{
    /// <summary>
    /// The Student entity
    /// </summary>
    public sealed class Student
    {
        /// <summary>
        /// The id of the student
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        public Name FirstName { get; private set; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string? Infix { get; private set; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public Name LastName { get; private set; }

        /// <summary>
        /// Constructor without id
        /// </summary>
        private Student(Name firstName, string? infix, Name lastName)
        {
            FirstName = firstName;
            Infix = infix;
            LastName = lastName;
        }

        public static Result<Student> TryCreate(string firstName, string? infix, string lastName)
        {
            Result<Name> firstNameResult = Name.CreateFromString(firstName);
            Result<Name> lastNameResult = Name.CreateFromString(lastName);

            if (!firstNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(firstName), "Firstname is not valid");
                return Result<Student>.Fail<Student>(validationMessage);
            }

            if (!lastNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(lastName), "Lastname is not valid");
                return Result<Student>.Fail<Student>(validationMessage);
            }

            return Result<Student>.Ok<Student>(new Student(firstNameResult.Value!, infix, lastNameResult.Value!));
        }
    }
}
