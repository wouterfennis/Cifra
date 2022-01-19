using AutoMapper;
using Cifra.Application.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Database.UnitTests.Mapping
{
    [TestClass]
    public class DatabaseProfileTests
    {
        [TestMethod]
        public void AssertConfigurationIsValid_WithDatabaseProfile_NoMappingFaults()
        {
            // Arrange
            var profile = new DatabaseProfile();
            var sut = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Act Assert
            sut.AssertConfigurationIsValid();
        }
    }
}
