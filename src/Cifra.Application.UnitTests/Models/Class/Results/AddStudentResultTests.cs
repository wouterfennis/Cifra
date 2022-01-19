using Cifra.Application.Models.Class.Results;
using Cifra.Core.Models.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Cifra.Application.UnitTests.Models.Class.Results
{
    [TestClass]
   public class AddStudentResultTests
    {
        [TestMethod]
        public void Constructor_WithValidationMessagesNull_ThrowsException()
        {
            IEnumerable<ValidationMessage> input = null;

            Action action = () => new AddStudentResult(input);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WithValidationMessageNull_ThrowsException()
        {
            ValidationMessage input = null;

            Action action = () => new AddStudentResult(input);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
