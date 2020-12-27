﻿using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.TestUtilities
{
    public class TestBuilder
    {
        private readonly Fixture _fixture;
        private Grade _minimumGrade;
        private readonly List<Assignment> _assignments;

        public TestBuilder()
        {
            _assignments = new List<Assignment>();
            _fixture = new Fixture();
        }

        public TestBuilder WithMinimumGrade(Grade grade)
        {
            _minimumGrade = grade;
            return this;
        }

        public TestBuilder WithRandomAssignments()
        {
            for (int i = 0; i < 3; i++)
            {
                var assignment = new AssignmentBuilder()
                    .WithRandomQuestions()
                    .Build();
                _assignments.Add(assignment);
            }            
            return this;
        }

        public Test Build()
        {
            var testName = _fixture.Create<Name>();
            var standardizationFactor = _fixture.Create<StandardizationFactor>();
            return new Test(Guid.NewGuid(), testName, standardizationFactor, _minimumGrade, _assignments);
        }
    }
}
