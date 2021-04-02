using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Models.ValueTypes
{
    [TestClass]
    public class GradeTests
    {
        [TestMethod]
        public void CreateFromByte_WithValidMinimumGrade_ReturnsGrade()
        {
            byte input = 1;

            var result = Grade.CreateFromByte(input);

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromByte_WithValidMaximumGrade_ReturnsGrade()
        {
            byte input = 10;

            var result = Grade.CreateFromByte(input);

            result.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromByte_WithTooHighGrade_ThrowsException()
        {
            byte input = 11;

            Action action = () => Grade.CreateFromByte(input);

            action.Should().Throw<ArgumentException>()
                .WithMessage($"The value: {input} is not within 0 and 10");
        }

        [TestMethod]
        public void Equals_TwoSeperateGradesWithSameValue_AreEqual()
        {
            byte input = 10;

            var grade1 = Grade.CreateFromByte(input);
            var grade2 = Grade.CreateFromByte(input);

            grade1.Equals(grade2);
        }
    }
}
