using AutoFixture;
using Cifra.Core.Models.Test;
using Cifra.Core.Models.ValueTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
        public void Constructor_ExistingClassWithAssignmentsNull_ThrowsException()
        {
            Action action = () => new Core.Models.Test.Test(
                _fixture.Create<int>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromInteger(_fixture.Create<int>()),
                Grade.CreateFromInteger(5),
                null,
                1);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddAssignment_WithAssignment_AddsAssignmentToTest()
        {
            Core.Models.Test.Test sut = CreateDefaultTest();

            var expectedAssignment = new Assignment(1);

            sut.AddAssignment(expectedAssignment);

            sut.Assignments.Should().ContainSingle();
            sut.Assignments.Single().Should().Be(expectedAssignment);
        }

        private Core.Models.Test.Test CreateDefaultTest()
        {
            return new Core.Models.Test.Test(
                _fixture.Create<int>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromInteger(_fixture.Create<int>()),
                Grade.CreateFromInteger(5),
                _fixture.CreateMany<Assignment>(0).ToList(),
                1);
        }
    }
}

