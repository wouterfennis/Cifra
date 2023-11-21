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
        public void ImplicitFromGradeToInt_WithValidValue_ConvertsToInt()
        {
            // Arrange
            Grade input = Grade.CreateFromInteger(10).Value;

            // Act
            int value = input;

            // Assert
            value.Should().Be(input.Value);
        }
    }
}
