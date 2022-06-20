using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Cifra.Core.Models.Class;

namespace Cifra.TestUtilities.Core
{
    [ExcludeFromCodeCoverage] // Part of test project.
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
