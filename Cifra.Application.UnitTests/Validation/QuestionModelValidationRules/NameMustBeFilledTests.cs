using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.UnitTests.Validation.QuestionModelValidationRules
{
    [TestClass]
    public class NameMustBeFilledTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }
    }
}
