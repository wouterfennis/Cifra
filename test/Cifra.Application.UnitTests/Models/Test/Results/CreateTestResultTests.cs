using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Cifra.Application.UnitTests.Models.Class.Results
{
    [TestClass]
    public class CreateTestResultTests
    {
        [TestMethod]
        public void Constructor_WithValidationMessagesNull_ThrowsException()
        {
            IEnumerable<ValidationMessage> input = null;

            Action action = () => new CreateTestResult(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WithValidationMessageNull_ThrowsException()
        {
            ValidationMessage input = null;

            Action action = () => new CreateTestResult(input);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
