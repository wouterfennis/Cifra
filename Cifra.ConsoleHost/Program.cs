using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using Cifra.FileSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cifra.ConsoleHost
{
    internal class Program
    {
        private ClassController _classController;
        private TestController _testController;

        public Program()
        {
            var compositionRoot = CompositionRoot();
            _classController = CompositionRoot().GetService<ClassController>();
            _testController = CompositionRoot().GetService<TestController>();
        }

        internal static void Main(string[] args)
        {

        }

        private static IServiceProvider CompositionRoot()
        {
            IConfigurationSection configuration = SetupAppsettings();
            string classRepositoryLocation = configuration["ClassRepository"];
            string testRepositoryLocation = configuration["TestRepository"];
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IValidator<CreateClassRequest>, Validator<CreateClassRequest>>()
                .AddTransient<IValidator<CreateTestRequest>, Validator<CreateTestRequest>>()
                .AddTransient<IFileLocationProvider>(x =>
                    new FileLocationProvider(FilePath.CreateFromString(classRepositoryLocation), FilePath.CreateFromString(testRepositoryLocation))
                    )
                .AddTransient<ITestRepository, TestRepository>()
                .AddTransient<ITestRepository, ClassRepository>()
                .AddScoped<ClassController>()
                .AddScoped<TestController>()
                .BuildServiceProvider();

            return serviceProvider;
        }

        private static IConfigurationSection SetupAppsettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .Build();

            return configuration.GetSection("Appsettings");
        }
    }
}
