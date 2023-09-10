using AutoMapper;
using Cifra.Api.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cifra.Api.UnitTests.Mapping
{
    [TestClass]
    public class ApiProfileTests
    {
        private IMapper _sut = default!;

        [TestInitialize]
        public void Initialize()
        {
            var profile = new ApiProfile();
            var sut = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _sut = sut.CreateMapper();
        }

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
