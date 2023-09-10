﻿using AutoFixture;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.TestUtilities.Domain
{
    [ExcludeFromCodeCoverage] // Part of test project.
    public class StudentBuilder
    {
        private readonly Fixture _fixture;

        public StudentBuilder()
        {
            _fixture = new Fixture();
        }

        public Student BuildRandomStudent()
        {
            string firstName = _fixture.Create<string>();
            string infix = _fixture.Create<string>();
            string lastName = _fixture.Create<string>();
            return new Student(Name.CreateFromString(firstName), infix, Name.CreateFromString(lastName));
        }
    }
}