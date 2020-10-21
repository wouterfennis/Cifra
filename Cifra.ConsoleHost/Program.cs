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
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.WriteLine("Test");
            var application = CompositionRoot().Resolve<Application>();
            await application.StartAsync();
        }

        private static IContainer CompositionRoot()
        {
            IConfigurationSection configuration = SetupAppsettings();
            string classRepositoryLocation = configuration["ClassRepository"];
            string testRepositoryLocation = configuration["TestRepository"];
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<ClassController>();
            builder.RegisterType<TestController>();
            builder.RegisterType<TestRepository>().AsImplementedInterfaces();
            builder.RegisterType<ClassRepository>().AsImplementedInterfaces();

            builder.RegisterType<Validator<CreateClassRequest>>().As<IValidator<CreateClassRequest>>();
            builder.RegisterType<Validator<AddStudentRequest>>().As<IValidator<AddStudentRequest>>();
            builder.RegisterType<Validator<CreateTestRequest>>().As<IValidator<CreateTestRequest>>();
            builder.RegisterType<Validator<AddQuestionRequest>>().As<IValidator<AddQuestionRequest>>();
            builder.RegisterType<NamesMustBeFilled>().As<IValidationRule<AddQuestionRequest>>();
            builder.RegisterType<TestIdMustBeFilled>().As<IValidationRule<AddQuestionRequest>>();
            builder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.NameMustBeFilled>().As<IValidationRule<AddStudentRequest>>();
            builder.RegisterType<Cifra.Application.Validation.TestModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateTestRequest>>();
            builder.RegisterType<Cifra.Application.Validation.ClassModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateClassRequest>>();
            var fileLocationProvider = new FileLocationProvider(
                FilePath.CreateFromString(classRepositoryLocation),
                FilePath.CreateFromString(testRepositoryLocation)
                );
            builder.RegisterInstance(fileLocationProvider).AsImplementedInterfaces();

            builder.RegisterModule<FileSystemModule>();

            return builder.Build();
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
