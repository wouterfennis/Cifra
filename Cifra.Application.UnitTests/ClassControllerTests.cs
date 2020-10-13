using AutoFixture;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Validation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.Application.UnitTests
{
    [TestClass]
    public class ClassControllerTests
    {
        private Fixture _fixture;
        private Mock<IClassRepository> _classRepository;
        private Mock<IValidator<CreateClassRequest>> _classValidator;
        private Mock<IValidator<AddStudentRequest>> _studentValidator;
        private ClassController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _classRepository = new Mock<IClassRepository>();
            _classValidator = new Mock<IValidator<CreateClassRequest>>();
            _studentValidator = new Mock<IValidator<AddStudentRequest>>();
            _sut = new ClassController(_classRepository.Object, _classValidator.Object, _studentValidator.Object);
        }

        [TestMethod]
        public void CreateClass_WithValidationMessages_ReturnsValidationMessages()
        {
            CreateClassRequest input = null;

            Action action = () => _sut.CreateClass(input);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
