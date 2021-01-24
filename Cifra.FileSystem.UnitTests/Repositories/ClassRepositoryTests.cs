using AutoFixture;
using Cifra.Application.Models.Class;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.FileSystem.UnitTests.Repositories
{
    [TestClass]
    public class TestsRepositoryTests
    {
        private Fixture _fixture;
        private Mock<IFileLocationProvider> _fileLocationProvider;
        private Mock<IFileInfoWrapperFactory> _fileInfoWrapperFactory;
        private ClassRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _fileInfoWrapperFactory = new Mock<IFileInfoWrapperFactory>();

            _sut = new ClassRepository(_fileLocationProvider.Object, _fileInfoWrapperFactory.Object);
        }

        [TestMethod]
        public async Task CreateAsync_WithValidClass_AddClassToRepositoryAsync()
        {
            // Arrange
            var classRepositoryPath = _fixture.Create<Application.Models.ValueTypes.Path>();
            _fileLocationProvider.Setup(x => x.GetClassRepositoryPath())
                .Returns(classRepositoryPath);

            var memoryStream = SetupNonExistingFile(classRepositoryPath);

            Class input = _fixture.Create<Class>();

            // Act
            await _sut.CreateAsync(input);

            // Assert
            var actualMemoryStream = new MemoryStream(memoryStream.ToArray());
            using (StreamReader reader = new StreamReader(actualMemoryStream))
            {
                var actualClasses = JsonConvert.DeserializeObject<List<FileEntity.Class>>(await reader.ReadToEndAsync());
                actualClasses.Should().NotBeNull();
                actualClasses.Should().ContainSingle();
                var actualClass = actualClasses.Single();
                actualClass.Id.Should().Be(input.Id);
                actualClass.Name.Should().Be(input.Name.Value);
            }
        }

        private MemoryStream SetupExistingFile(Application.Models.ValueTypes.Path classRepositoryPath)
        {
            var memoryStream = new MemoryStream();
            var file = new Mock<IFileInfoWrapper>();
            file.SetupGet(x => x.Exists)
                .Returns(true);
            file.Setup(x => x.OpenRead())
                .Returns(memoryStream);
            file.Setup(x => x.OpenWrite())
                .Returns(memoryStream);
            _fileInfoWrapperFactory.Setup(x => x.Create(classRepositoryPath))
                .Returns(file.Object);
            return memoryStream;
        }

        private MemoryStream SetupNonExistingFile(Application.Models.ValueTypes.Path classRepositoryPath)
        {
            var memoryStream = new MemoryStream();
            var file = new Mock<IFileInfoWrapper>();
            file.SetupGet(x => x.Exists)
                .Returns(false);
            file.Setup(x => x.OpenWrite())
                .Returns(memoryStream);
            _fileInfoWrapperFactory.Setup(x => x.Create(classRepositoryPath))
                .Returns(file.Object);
            return memoryStream;
        }
    }
}
