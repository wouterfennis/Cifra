using Cifra.FileSystem.FileReaders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cifra.FileSystem.UnitTests.FileReaders
{
    [TestClass]
    public class MagisterFileReaderTests
    {
        private MagisterFileReader _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new MagisterFileReader();
        }

        [TestMethod]
        public void ReadClass_WithValidCsv_ReturnsClass()
        {
            var validCsv = "Stamnummer,Klas,Roepnaam,Tussenvoegsel,Achternaam,Studie,Email,Telefoonnummer";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(validCsv));
            var file = new Mock<IFileInfoWrapper>();
            file.Setup(x => x.OpenRead())
                .Returns(stream);

            var result = _sut.ReadClass(file.Object);

            result.Should().NotBeNull();
        }
    }
}
