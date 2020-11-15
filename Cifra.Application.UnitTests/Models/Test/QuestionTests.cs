using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.UnitTests.Models.Test
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void Constructor_WithNoQuestionNames_ThrowsException()
        {
            Action action = () => new Question(null, QuestionScore.CreateFromByte(1));

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
