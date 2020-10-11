using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cifra.Application.UnitTests.Models.Test
{
    [TestClass]
    public class TestTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Constructor_NewClass_GeneratesId()
        {
            var result = new Application.Models.Test.Test(
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5));

            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
        }

        [TestMethod]
        public void Constructor_ExistingClassWithQuestionsNull_ThrowsException()
        {
            Action action = () => new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddQuestion_WithQuestion_AddsQuestionToTest()
        {
            var sut = new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                _fixture.CreateMany<Question>(0).ToList());

            var expectedQuestion = new Question(_fixture.CreateMany<Name>(0).ToList(),
                QuestionScore.CreateFromByte(_fixture.Create<byte>()));

            sut.AddQuestion(expectedQuestion);

            sut.Questions.Should().ContainSingle();
            sut.Questions.Single().Should().Be(expectedQuestion);
        }

        [TestMethod]
        public void AddQuestion_WithQuestionNull_ThrowsException()
        {
            var sut = new Application.Models.Test.Test(
                _fixture.Create<Guid>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(_fixture.Create<byte>()),
                Grade.CreateFromByte(5),
                _fixture.CreateMany<Question>(0).ToList());

            Action action = () => sut.AddQuestion(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
