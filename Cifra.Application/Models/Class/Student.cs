using Cifra.Application.Models.ValueTypes;

namespace Cifra.Application.Models
{
    /// <summary>
    /// The Student entity
    /// </summary>
    public class Student
    {
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
