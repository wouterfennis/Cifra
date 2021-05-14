using AutoFixture;
using Cifra.FileSystem.FileReaders;
using Cifra.FileSystem.FileSystemInfo;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        private Mock<IFileInfoWrapperFactory> _fileInfoWrapperFactoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fileInfoWrapperFactoryMock = new Mock<IFileInfoWrapperFactory>();
            _sut = new MagisterFileReader(_fileInfoWrapperFactoryMock.Object);
        }

        [TestMethod]
        public void ReadClass_WithValidCsv_ReturnsClass()
        {
            // Arrange
            string expectedClassName = _fixture.Create<string>();
            string firstname = _fixture.Create<string>();
            string infix = _fixture.Create<string>();
            string lastName = _fixture.Create<string>();
            Application.Models.ValueTypes.Path filePath = _fixture.Create<Application.Models.ValueTypes.Path>();
            var validCsv = "Stamnummer,Klas,Roepnaam,Tussenvoegsel,Achternaam,Studie,Email,Telefoonnummer\n" +
                $"{_fixture.Create<int>()}," +
                $"\"{expectedClassName}\",\"{firstname}\",\"{infix}\",\"{lastName}\"," +
                $"\"{_fixture.Create<string>()}\",\"{_fixture.Create<string>()}\",{_fixture.Create<int>()}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(validCsv));
            var fileInfoWrapper = new Mock<IFileInfoWrapper>();
            fileInfoWrapper.Setup(x => x.OpenRead())
                .Returns(stream);
            _fileInfoWrapperFactoryMock.Setup(x => x.Create(filePath))
                .Returns(fileInfoWrapper.Object);

            // Act
            Application.Models.Class.Magister.MagisterClass result = _sut.ReadClass(filePath);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(expectedClassName);
        }

        [TestMethod]
        public void ReadClass_WithValidCsv_ReturnsStudent()
        {
            // Arrange
            string className = _fixture.Create<string>();
            string expectedFirstName = _fixture.Create<string>();
            string expectedInfix = _fixture.Create<string>();
            string expectedLastName = _fixture.Create<string>();
            Application.Models.ValueTypes.Path filePath = _fixture.Create<Application.Models.ValueTypes.Path>();
            var validCsv = "Stamnummer,Klas,Roepnaam,Tussenvoegsel,Achternaam,Studie,Email,Telefoonnummer\n" +
                $"{_fixture.Create<int>()}," +
                $"\"{className}\",\"{expectedFirstName}\",\"{expectedInfix}\",\"{expectedLastName}\"," +
                $"\"{_fixture.Create<string>()}\",\"{_fixture.Create<string>()}\",{_fixture.Create<int>()}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(validCsv));
            var fileInfoWrapper = new Mock<IFileInfoWrapper>();
            fileInfoWrapper.Setup(x => x.OpenRead())
                .Returns(stream);
            _fileInfoWrapperFactoryMock.Setup(x => x.Create(filePath))
                .Returns(fileInfoWrapper.Object);

            // Act
            Application.Models.Class.Magister.MagisterClass result = _sut.ReadClass(filePath);

            // Assert
            result.Should().NotBeNull();
            result.Students.Should().ContainSingle();

            var actualStudent = result.Students.Single();
            actualStudent.FirstName.Should().Be(expectedFirstName);
            actualStudent.Infix.Should().Be(expectedInfix);
            actualStudent.LastName.Should().Be(expectedLastName);
        }
    }
}
