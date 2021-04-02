﻿using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.Test.Results;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cifra.Application.UnitTests.TestServiceTests
{
    [TestClass]
    public class AddAssignmentAsyncTests
    {
        private Fixture _fixture;
        private Mock<ITestRepository> _testRepository;
        private Mock<IValidator<CreateTestRequest>> _testValidator;
        private Mock<IValidator<AddAssignmentRequest>> _assignmentValidator;
        private TestService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testRepository = new Mock<ITestRepository>();
            _testValidator = new Mock<IValidator<CreateTestRequest>>();
            _assignmentValidator = new Mock<IValidator<AddAssignmentRequest>>();
            _sut = new TestService(_testRepository.Object, _testValidator.Object, _assignmentValidator.Object);
        }

        [TestMethod]
        public async Task AddAssignment_WithValidationMessages_ReturnsValidationMessages()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.Should().NotBeNull();
            result.ValidationMessages.Should().BeEquivalentTo(expectedValidationMessages);
        }

        [TestMethod]
        public async Task AddAssignment_WithValidationMessages_DoesNotAddStudent()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>();
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            _testRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddAssignment_TestDoesNotExists_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var expectedValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(expectedValidationMessages);

            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync((Test)null);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            ValidationMessage validationMessage = result.ValidationMessages.Single();
            validationMessage.Field.Should().Be("TestId");
            validationMessage.Message.Should().Be("No test was found");
        }

        [TestMethod]
        public async Task AddAssignment_UpdateFails_ReturnsValidationMessage()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            Test expectedTest = CreateDefaultTest();
            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedTest);

            var testValidationMessage = _fixture.Create<ValidationMessage>();
            _testRepository
                .Setup(x => x.UpdateAsync(expectedTest))
                .ReturnsAsync(testValidationMessage);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.ValidationMessages.Should().ContainSingle();
            result.ValidationMessages.Should().Contain(testValidationMessage);
        }

        [TestMethod]
        public async Task AddAssignment_UpdateSucceeds_ReturnsResult()
        {
            var input = _fixture.Create<AddAssignmentRequest>();
            var assignmentValidationMessages = _fixture.CreateMany<ValidationMessage>(0);
            _assignmentValidator
                .Setup(x => x.ValidateRules(input))
                .Returns(assignmentValidationMessages);

            Test expectedTest = CreateDefaultTest();
            _testRepository
                .Setup(x => x.GetAsync(input.TestId))
                .ReturnsAsync(expectedTest);

            _testRepository
                .Setup(x => x.UpdateAsync(expectedTest))
                .ReturnsAsync((ValidationMessage)null);

            AddAssignmentResult result = await _sut.AddAssignmentAsync(input);

            result.AssignmentId.Should().NotBeEmpty();
            result.TestId.Should().Be(expectedTest.Id);
            result.ValidationMessages.Should().BeEmpty();
        }

        private Test CreateDefaultTest()
        {
            return new Test(
                Name.CreateFromString(_fixture.Create<string>()),
                StandardizationFactor.CreateFromByte(5),
                Grade.CreateFromByte(4),
                1);
        }
    }
}