using AutoFixture;
using Cifra.TestUtilities.Autofac;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Cifra.ConsoleHost.UnitTests
{
    [TestClass]
    public class DependencyInjectionTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Resolve_WithFileSystemModule_ShouldNotThrowException()
        {
            // Arrange
           var appsettings = SetupAppsettings();
           var container = DependencyInjection.RegisterDependencies(appsettings);

           var scope = container.BeginLifetimeScope();

           Action action = () => scope.ResolveAll();

           action.Should().NotThrow();
        }

        private IConfigurationSection SetupAppsettings()
        {
            var appsettings = JsonConvert.SerializeObject(new
            {
                Appsettings = new
                {
                    ClassRepository = _fixture.Create<string>(),
                    TestRepository = _fixture.Create<string>(),
                    SpreadsheetDirectory = _fixture.Create<string>(),
                    MagisterDirectory = _fixture.Create<string>(),
                }
            });

            var builder = new ConfigurationBuilder();

            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appsettings)));

            return builder.Build().GetSection("Appsettings");
        }
    }
}
