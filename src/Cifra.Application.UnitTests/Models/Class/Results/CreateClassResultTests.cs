using Cifra.Application.Models.Class.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Models.Class.Results
{
    [TestClass]
   public class CreateClassResultTests
    {
        [TestMethod]
        public void Constructor_WithValidationMessagesNull_ThrowsException()
        {
            Action action = () => new CreateClassResult(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
