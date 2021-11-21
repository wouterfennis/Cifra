using System;
using System.Collections.Generic;
using AutoFixture;
using Cifra.Application.Models.Class;

namespace Cifra.TestUtilities.Application
{
    public class ClassBuilder
    {
        private readonly Fixture _fixture;

        public ClassBuilder()
        {
            _fixture = new Fixture();
        }

        public Class BuildRandomClass()
        {
            int id = _fixture.Create<int>();
            string className = _fixture.Create<string>();
            List<Student> students = WithRandomStudents();
            return new Class(id, className, students);
        }

        private List<Student> WithRandomStudents()
        {
            var students = new List<Student>();
            for (int i = 0; i < 3; i++)
            {
                var student = new StudentBuilder()
                    .BuildRandomStudent();
                students.Add(student);
            }
            return students;
        }
    }
}
