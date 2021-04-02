using Cifra.Application.Models.Class.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cifra.Application.UnitTests.Models.Class.Results
{
    [TestClass]
   public class GetAllClassesResultTests
    {
        [TestMethod]
        public void Constructor_WithValidationMessagesNull_ThrowsException()
        {
            Action action = () => new GetAllClassesResult(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
