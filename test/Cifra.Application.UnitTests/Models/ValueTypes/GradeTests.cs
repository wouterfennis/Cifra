using Cifra.Domain.ValueTypes;
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

            result.Value.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromInteger_WithValidMaximumGrade_ReturnsGrade()
        {
            int input = 10;

            var result = Grade.CreateFromInteger(input);

            result.Value.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromInteger_WithTooHighGrade_ResultFails()
        {
            int input = 11;

            var result = Grade.CreateFromInteger(input);

            result.IsSuccess.Should().BeFalse();
            result.ValidationMessage.Message.Should().Be($"Minimum grade must be from 1 to 10");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateFromInteger_WithTooLowGrade_ResultFails()
        {
            int input = 0;

            var result = Grade.CreateFromInteger(input);

            result.IsSuccess.Should().BeFalse();
            result.ValidationMessage.Message.Should().Be($"Minimum grade must be from 1 to 10");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void ImplicitFromGradeToDecimal_WithValidIntegerValue_ConvertsToDecimal()
        {
            // Arrange
            Grade input = Grade.CreateFromInteger(10).Value;

            // Act
            decimal value = input;

            // Assert
            value.Should().Be(input.Value);
        }

        [TestMethod]
        public void CreateFromDecimal_WithValidMinimumGrade_ReturnsGrade()
        {
            decimal input = 1.1m;

            var result = Grade.CreateFromDecimal(input);

            result.Value.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromDecimal_WithValidMaximumGrade_ReturnsGrade()
        {
            decimal input = 9.9m;

            var result = Grade.CreateFromDecimal(input);

            result.Value.Value.Should().Be(input);
        }

        [TestMethod]
        public void CreateFromDecimal_WithTooHighGrade_ResultFails()
        {
            decimal input = 10.1m;

            var result = Grade.CreateFromDecimal(input);

            result.IsSuccess.Should().BeFalse();
            result.ValidationMessage.Message.Should().Be($"Minimum grade must be from 1 to 10");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void CreateFromDecimal_WithTooLowGrade_ResultFails()
        {
            decimal input = 0.9m;

            var result = Grade.CreateFromDecimal(input);

            result.IsSuccess.Should().BeFalse();
            result.ValidationMessage.Message.Should().Be($"Minimum grade must be from 1 to 10");
            result.Value.Should().BeNull();
        }

        [TestMethod]
        public void ImplicitFromGradeToDecimal_WithValidDecimalValue_ConvertsToDecimal()
        {
            // Arrange
            Grade input = Grade.CreateFromDecimal(8.5m).Value;

            // Act
            decimal value = input;

            // Assert
            value.Should().Be(input.Value);
        }
    }
}
