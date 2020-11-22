using Cifra.Application.Models.ValueTypes;

namespace Cifra.Application.Models.Class
{
    /// <summary>
    /// The Student entity
    /// </summary>
    public sealed class Student
    {
        /// <summary>
        /// The first name of the student
        /// </summary>
        public Name FirstName { get; }

        /// <summary>
        /// The infix of the student
        /// </summary>
        public Name Infix { get; }

        /// <summary>
        /// The last name of the student
        /// </summary>
        public Name LastName { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Student(Name firstName, Name infix, Name lastName)
        {
            FirstName = firstName;
            Infix = infix;
            LastName = lastName;
        }
    }
}
