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
        public int Id { get; }

        /// <summary>
        /// The first name of the student
        /// </summary>
        public Name FirstName { get; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public string? Infix { get; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public Name LastName { get; }

        /// <summary>
        /// Constructor without id
        /// </summary>
        public Student(Name firstName, string? infix, Name lastName)
        {
            FirstName = firstName;
            Infix = infix;
            LastName = lastName;
        }

        /// <summary>
        /// Constructor with id
        /// </summary>
        public Student(int id, Name firstName, string? infix, Name lastName)
        {
            Id = id;
            FirstName = firstName;
            Infix = infix;
            LastName = lastName;
        }
    }
}
