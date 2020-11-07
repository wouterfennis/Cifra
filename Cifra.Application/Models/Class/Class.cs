using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class
{
    /// <summary>
    /// The Class entity
    /// </summary>
    public sealed class Class
    {
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The Name of the Class
        /// </summary>
        public Name Name { get; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public List<Student> Students { get; }

        /// <summary>
        /// Constructor for a new class
        /// </summary>
        internal Class(Name className)
        {
            Id = Guid.NewGuid();
            Name = className;
            Students = new List<Student>();
        }

        /// <summary>
        /// Constructor for existing tests
        /// </summary>
        public Class(Guid id, Name className, List<Student> students)
        {
            Id = id;
            Name = className;
            Students = students ?? throw new ArgumentNullException(nameof(students));
        }

        /// <summary>
        /// Adds a student to the class
        /// </summary>
        internal void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            Students.Add(student);
        }
    }
}
