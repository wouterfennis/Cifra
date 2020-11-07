using Cifra.Application.Models.ValueTypes;

namespace Cifra.Application.Models.Class
{
    /// <summary>
    /// The Student entity
    /// </summary>
    public sealed class Student
    {
        /// <summary>
        /// The Full Name of the student
        /// </summary>
        public Name FullName { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Student(Name fullName)
        {
            FullName = fullName;
        }
    }
}
