using AutoFixture;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
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
            Action action = () => new Application.Models.Test.Test(
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
            Application.Models.Test.Test sut = CreateDefaultTest();

            var expectedAssignment = new Assignment(1);

            sut.AddAssignment(expectedAssignment);

            sut.Assignments.Should().ContainSingle();
            sut.Assignments.Single().Should().Be(expectedAssignment);
        }

        private Application.Models.Test.Test CreateDefaultTest()
        {
            return new Application.Models.Test.Test(
                _fixture.Create<int>(),
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromInteger(_fixture.Create<int>()),
                Grade.CreateFromInteger(5),
                _fixture.CreateMany<Assignment>(0).ToList(),
                1);
        }
    }
}

