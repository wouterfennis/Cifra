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
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.WriteLine("Test");
            var application = CompositionRoot().GetService<Application>();
            await application.StartAsync();
        }

        private static IServiceProvider CompositionRoot()
        {
            IConfigurationSection configuration = SetupAppsettings();
            string classRepositoryLocation = configuration["ClassRepository"];
            string testRepositoryLocation = configuration["TestRepository"];
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IValidator<CreateClassRequest>, Validator<CreateClassRequest>>()
                .AddTransient<IValidator<AddStudentRequest>, Validator<AddStudentRequest>>()
                .AddTransient<IValidator<CreateTestRequest>, Validator<CreateTestRequest>>()
                .AddTransient<IValidator<AddQuestionRequest>, Validator<AddQuestionRequest>>()
                .AddTransient<IFileLocationProvider>(x =>
                    new FileLocationProvider(FilePath.CreateFromString(classRepositoryLocation), FilePath.CreateFromString(testRepositoryLocation))
                    )
                .AddTransient<ITestRepository, TestRepository>()
                .AddTransient<IClassRepository, ClassRepository>()
                .AddScoped<ClassController>()
                .AddScoped<TestController>()
                .AddScoped<Application>()
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
