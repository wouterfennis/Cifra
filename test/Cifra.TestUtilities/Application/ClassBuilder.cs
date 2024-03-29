﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Cifra.Domain;

namespace Cifra.TestUtilities.Domain
{
    [ExcludeFromCodeCoverage(Justification = " Part of test project.")]
    public class ClassBuilder
    {
        private readonly Fixture _fixture;

        public ClassBuilder()
        {
            _fixture = new Fixture();
        }

        public Class BuildRandomClass()
        {
            uint id = _fixture.Create<uint>();
            string className = _fixture.Create<string>();
            List<Student> students = WithRandomStudents();
            return Class.TryCreate(id, className, students).Value!;
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
