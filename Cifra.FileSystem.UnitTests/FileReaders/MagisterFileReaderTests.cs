using AutoFixture;
using Cifra.FileSystem.FileReaders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cifra.FileSystem.UnitTests.FileReaders
{
    [TestClass]
    public class MagisterFileReaderTests
    {
        private Fixture _fixture;
        private MagisterFileReader _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new MagisterFileReader();
        }

        [TestMethod]
        public void ReadClass_WithValidCsv_ReturnsClass()
        {
            string expectedClassName = _fixture.Create<string>();
            string expectedFirstName = _fixture.Create<string>();
            string expectedInfix = _fixture.Create<string>();
            string expectedLastName = _fixture.Create<string>();
            var validCsv = "Stamnummer,Klas,Roepnaam,Tussenvoegsel,Achternaam,Studie,Email,Telefoonnummer\n" +
                $"{_fixture.Create<int>()}," +
                $"\"{expectedClassName}\",\"{expectedFirstName}\",\"{expectedInfix}\",\"{expectedLastName}\"," +
                $"\"{_fixture.Create<string>()}\",\"{_fixture.Create<string>()}\",{_fixture.Create<int>()}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(validCsv));
            var file = new Mock<IFileInfoWrapper>();
            file.Setup(x => x.OpenRead())
                .Returns(stream);

            var result = _sut.ReadClass(file.Object);

            result.Should().NotBeNull();
            result.Id.Should().BeEmpty();
            result.Name.Should().Be(expectedClassName);
        }

        [TestMethod]
        public void ReadClass_WithValidCsv_ReturnsStudent()
        {
            string expectedClassName = _fixture.Create<string>();
            string expectedFirstName = _fixture.Create<string>();
            string expectedInfix = _fixture.Create<string>();
            string expectedLastName = _fixture.Create<string>();
            var validCsv = "Stamnummer,Klas,Roepnaam,Tussenvoegsel,Achternaam,Studie,Email,Telefoonnummer\n" +
                $"{_fixture.Create<int>()}," +
                $"\"{expectedClassName}\",\"{expectedFirstName}\",\"{expectedInfix}\",\"{expectedLastName}\"," +
                $"\"{_fixture.Create<string>()}\",\"{_fixture.Create<string>()}\",{_fixture.Create<int>()}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(validCsv));
            var file = new Mock<IFileInfoWrapper>();
            file.Setup(x => x.OpenRead())
                .Returns(stream);

            var result = _sut.ReadClass(file.Object);

            result.Should().NotBeNull();
            result.Students.Should().ContainSingle();

            var actualStudent = result.Students.Single();
            actualStudent.FirstName.Should().Be(expectedFirstName);
            actualStudent.Infix.Should().Be(expectedInfix);
            actualStudent.LastName.Should().Be(expectedLastName);
        }
    }
}
