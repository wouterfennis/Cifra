using Autofac;
using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using Cifra.Application.Validation.QuestionModelValidationRules;
using Cifra.Application.Validation.StudentModelValidationRules;
using Cifra.Application.Validation.TestModelValidationRules;
using Cifra.FileSystem;
using Cifra.FileSystem.Repositories;
using Cifra.FileSystem.Spreadsheet;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("This is a test version. Not ready for actual use");
            Console.ResetColor();
            var application = CompositionRoot().Resolve<Application>();
            await application.StartAsync();
        }

        private static IContainer CompositionRoot()
        {
            IConfigurationSection configuration = SetupAppsettings();
            string classRepositoryPath = configuration["ClassRepository"];
            string testRepositoryPath = configuration["TestRepository"];
            string spreadsheetDirectoryPath = configuration["SpreadsheetDirectory"];
            string magisterDirectoryPath = configuration["MagisterDirectory"];
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<ClassController>();
            builder.RegisterType<TestController>();
            builder.RegisterType<TestRepository>().AsImplementedInterfaces();
            builder.RegisterType<ClassRepository>().AsImplementedInterfaces();
            builder.RegisterType<TestResultsSpreadsheetFactory>().AsImplementedInterfaces();

            builder.RegisterType<Validator<CreateClassRequest>>().As<IValidator<CreateClassRequest>>();
            builder.RegisterType<Validator<CreateMagisterClassRequest>>().As<IValidator<CreateMagisterClassRequest>>();
            builder.RegisterType<Validator<AddStudentRequest>>().As<IValidator<AddStudentRequest>>();
            builder.RegisterType<Validator<CreateTestRequest>>().As<IValidator<CreateTestRequest>>();
            builder.RegisterType<Validator<AddAssignmentRequest>>().As<IValidator<AddAssignmentRequest>>();
            builder.RegisterType<Validator<AddQuestionRequest>>().As<IValidator<AddQuestionRequest>>();
            builder.RegisterType<NamesMustBeFilled>().As<IValidationRule<AddQuestionRequest>>();
            builder.RegisterType<TestIdMustBeFilled>().As<IValidationRule<AddQuestionRequest>>();
            
            builder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.FirstNameMustBeFilled>().As<IValidationRule<AddStudentRequest>>();
            builder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.LastNameMustBeFilled>().As<IValidationRule<AddStudentRequest>>();
            builder.RegisterType<Cifra.Application.Validation.TestModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateTestRequest>>();
            builder.RegisterType<Cifra.Application.Validation.ClassModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateClassRequest>>();
            builder.RegisterType<Cifra.Application.Validation.MagisterClassModelValidationRules.FileLocationMustBeFilled>().As<IValidationRule<CreateMagisterClassRequest>>();
            var fileLocationProvider = new FileLocationProvider(
                Path.CreateFromString(classRepositoryPath),
                Path.CreateFromString(testRepositoryPath),
                Path.CreateFromString(spreadsheetDirectoryPath),
                Path.CreateFromString(magisterDirectoryPath)
                );
            builder.RegisterInstance(fileLocationProvider).AsImplementedInterfaces();

            builder.RegisterModule<AreaModule>();

            return builder.Build();
        }

        private static IConfigurationSection SetupAppsettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("./appsettings.json", false, true)
                .Build();

            return configuration.GetSection("Appsettings");
        }
    }
}
