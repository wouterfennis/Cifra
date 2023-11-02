using Cifra.Domain.Validation;
using Cifra.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var validationMessage = ValidationMessage.Create(nameof(className), "Class name cannot be empty");
                return Result<Class>.Fail<Class>(validationMessage);
            }

            return Result<Class>.Ok<Class>(new Class(nameResult.Value!));
        }

        public static Result<Class> TryCreate(uint id, string className, IEnumerable<Student> students)
        {
            var nameResult = Name.CreateFromString(className);

            if (!nameResult.IsSuccess)
            {
                var validationMessage = ValidationMessage.Create(nameof(className), "Class name cannot be empty");
                return Result<Class>.Fail<Class>(validationMessage);
            }

            return Result<Class>.Ok<Class>(new Class(id, nameResult.Value!, students.ToList()));
        }

        /// <summary>
        /// Update this instance of the class with properties from other class.
        /// </summary>
        public void UpdateFromOtherClass(Class otherClass)
        {
            Name = otherClass.Name;

            var originalStudentIds = Students.Select(x => x.Id);
            var updatedStudentIds = otherClass.Students.Select(x => x.Id);

            var studentIdsToRemove = originalStudentIds.Except(updatedStudentIds);
            var studentsToUpdate = originalStudentIds.Where(x => updatedStudentIds.Contains(x));
            var studentsToAdd = otherClass.Students.Where(x => !originalStudentIds.Contains(x.Id)).ToList();

            Students.RemoveAll(x => studentIdsToRemove.Contains(x.Id));

            foreach (var studentdToUpdate in studentsToUpdate)
            {
                var originalStudent = GetStudent(studentdToUpdate)!;
                var updatedStudent = otherClass.GetStudent(studentdToUpdate)!;
                originalStudent.UpdateFromOtherStudent(updatedStudent!);
            }

            Students.AddRange(studentsToAdd);
        }

        private Student? GetStudent(uint id)
        {
            return Students.SingleOrDefault(x => x.Id == id);
        }
    }
}
