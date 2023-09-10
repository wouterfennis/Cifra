using Autofac;
using AutoFixture;
using Cifra.TestUtilities.Autofac;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Resolve_WithApplicationDependencies_ShouldNotThrowException()
        {
            // Arrange
            IConfigurationSection appsettings = SetupAppsettings().GetSection("Appsettings");
            var containerBuilder = new ContainerBuilder();
            DependencyInjection.RegisterApplicationDependencies(containerBuilder, appsettings);
            IContainer container = containerBuilder.Build();

            var scope = container.BeginLifetimeScope();

            // Act
            Action action = () => scope.ResolveAll();

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void Resolve_WithLogging_ShouldNotThrowException()
        {
            // Arrange
            IConfigurationRoot appsettings = SetupAppsettings();
            var containerBuilder = new ContainerBuilder();
            DependencyInjection.RegisterLogging(containerBuilder, appsettings);
            IContainer container = containerBuilder.Build();

            var scope = container.BeginLifetimeScope();

            // Act
            Action action = () => scope.ResolveAll();

            // Assert
            action.Should().NotThrow();
        }

        private IConfigurationRoot SetupAppsettings()
        {
            var appsettings = JsonConvert.SerializeObject(new
            {
                Appsettings = new
                {
                    ClassRepository = _fixture.Create<string>(),
                    TestRepository = _fixture.Create<string>(),
                    SpreadsheetDirectory = _fixture.Create<string>(),
                    ClassesDirectory = _fixture.Create<string>(),
                }
            });

            var builder = new ConfigurationBuilder();

            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appsettings)));

            return builder.Build();
        }
    }
}
