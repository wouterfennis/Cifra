using AutoMapper;
using Cifra.Api.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Api.UnitTests.Mapping
{
    [TestClass]
    public class ApiProfileTests
    {
        [TestMethod]
        public void AssertConfigurationIsValid_WithApiProfile_NoMappingFaults()
        {
            // Arrange
            var profile = new ApiProfile();
            var sut = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            // Act Assert
            sut.AssertConfigurationIsValid();
        }
    }
}
