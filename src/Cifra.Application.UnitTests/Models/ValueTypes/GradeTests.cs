using Cifra.Core.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Models.ValueTypes
{
    [TestClass]
    public class GradeTests
    {
        [TestMethod]
        public void CreateFromInteger_WithValidMinimumGrade_ReturnsGrade()
        {
            int input = 1;

            var result = Grade.CreateFromInteger(input);

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromInteger_WithValidMaximumGrade_ReturnsGrade()
        {
            int input = 10;

            var result = Grade.CreateFromInteger(input);

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromInteger_WithTooHighGrade_ThrowsException()
        {
            int input = 11;

            Action action = () => Grade.CreateFromInteger(input);

            action.Should().Throw<ArgumentException>()
                .WithMessage($"The value: {input} is not within 0 and 10");
        }

        [TestMethod]
        public void Equals_TwoSeperateGradesWithSameValue_AreEqual()
        {
            int input = 10;

            var grade1 = Grade.CreateFromInteger(input);
            var grade2 = Grade.CreateFromInteger(input);

            grade1.Should().Equals(grade2);
        }
    }
}
