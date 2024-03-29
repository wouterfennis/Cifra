﻿using AutoFixture;
using Cifra.Domain;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.TestUtilities.Domain
{
    [ExcludeFromCodeCoverage(Justification = "Part of test project.")]
    public class StudentBuilder
    {
        private readonly Fixture _fixture;

        public StudentBuilder()
        {
            _fixture = new Fixture();
        }

        public Student BuildRandomStudent()
        {
            uint id = _fixture.Create<uint>();
            string firstName = _fixture.Create<string>();
            string infix = _fixture.Create<string>();
            string lastName = _fixture.Create<string>();
            return Student.TryCreate(id, firstName, infix, lastName).Value;
        }
    }
}
