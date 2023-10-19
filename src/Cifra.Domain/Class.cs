using Cifra.Domain.Validation;
using Cifra.Domain.ValueTypes;
using System;
using System.Collections.Generic;

namespace Cifra.Domain
{
    /// <summary>
    /// The Class entity
    /// </summary>
    public sealed class Class
    {
        /// <summary>
        /// The Id of the Class
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// The Name of the Class
        /// </summary>
        public Name Name { get; private set; }

        /// <summary>
        /// The Students of the Class
        /// </summary>
        public List<Student> Students { get; private set; }

        private Class()
        {
            // Only exists for Entity Framework
        }

        /// <summary>
        /// Constructor for a new class
        /// </summary>
        private Class(Name className)
        {
            Name = className;
            Students = new List<Student>();
        }

        /// <summary>
        /// Constructor for a existing class
        /// </summary>
        private Class(uint id, Name className, List<Student> students)
        {
            Id = id;
            Name = className;
            Students = students;
        }

        public static Result<Class> TryCreate(string className)
        {
            var nameResult = Name.CreateFromString(className);

            if (!nameResult.IsSuccess)
            {
                var validationMessage = ValidationMessage.Create(nameof(className), "Class name cannot be null");
                return Result<Class>.Fail<Class>(validationMessage);
            }

            return Result<Class>.Ok<Class>(new Class(nameResult.Value!));
        }

        public static Result<Class> TryCreate(uint id, string className, List<Student> students)
        {
            var nameResult = Name.CreateFromString(className);

            if (!nameResult.IsSuccess)
            {
                var validationMessage = ValidationMessage.Create(nameof(className), "Class name cannot be null");
                return Result<Class>.Fail<Class>(validationMessage);
            }

            return Result<Class>.Ok<Class>(new Class(id, nameResult.Value!, students));
        }

        /// <summary>
        /// Adds a student to the class
        /// </summary>
        public void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            Students.Add(student);
        }
    }
}
