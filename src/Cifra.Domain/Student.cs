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
        public uint Id { get; private set; }

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
        /// // Only exists for Entity Framework.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Student()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            // Only exists for Entity Framework
        }

        /// <summary>
        /// Constructor for new student
        /// </summary>
        private Student(Name firstName, string? infix, Name lastName)
        {
            FirstName = firstName;
            Infix = infix;
            LastName = lastName;
        }

        /// <summary>
        /// Constructor for existing student
        /// </summary>
        private Student(uint id, Name firstName, string? infix, Name lastName)
        {
            Id = id;
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
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(firstName), $"Firstname: '{firstName}' is not valid");
                return Result<Student>.Fail<Student>(validationMessage);
            }

            if (!lastNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(lastName), $"Lastname: '{lastName}' is not valid");
                return Result<Student>.Fail<Student>(validationMessage);
            }

            return Result<Student>.Ok<Student>(new Student(firstNameResult.Value!, infix, lastNameResult.Value!));
        }

        public static Result<Student> TryCreate(uint id, string firstName, string? infix, string lastName)
        {
            Result<Name> firstNameResult = Name.CreateFromString(firstName);
            Result<Name> lastNameResult = Name.CreateFromString(lastName);

            if (!firstNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(firstName), $"Firstname: '{firstName}' is not valid");
                return Result<Student>.Fail<Student>(validationMessage);
            }

            if (!lastNameResult.IsSuccess)
            {
                ValidationMessage validationMessage = ValidationMessage.Create(nameof(lastName), $"Lastname: '{lastName}' is not valid");
                return Result<Student>.Fail<Student>(validationMessage);
            }

            return Result<Student>.Ok<Student>(new Student(id, firstNameResult.Value!, infix, lastNameResult.Value!));
        }

        /// <summary>
        /// Update this instance of the student with properties from other student.
        /// </summary>
        public void UpdateFromOtherStudent(Student otherStudent)
        {
            FirstName = otherStudent.FirstName;
            Infix = otherStudent.Infix;
            LastName = otherStudent.LastName;
        }
    }
}
