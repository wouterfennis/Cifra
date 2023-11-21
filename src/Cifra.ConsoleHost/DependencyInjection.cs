using Autofac;
using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using Cifra.Application.Validation.AssignmentModelValidationRules;
using Cifra.FileSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using System.Reflection;

namespace Cifra.ConsoleHost
{
    /// <summary>
    /// Responsible for registering all the dependencies.
    /// </summary>
    internal static class DependencyInjection
    {

        /// <summary>
        /// Registers application dependencies to the container builder.
        /// </summary>
        internal static void RegisterApplicationDependencies(ContainerBuilder containerBuilder, IConfigurationSection appsettings)
        {
            string classRepositoryPath = appsettings["ClassRepository"];
            string testRepositoryPath = appsettings["TestRepository"];
            string spreadsheetDirectoryPath = appsettings["SpreadsheetDirectory"];
            string magisterDirectoryPath = appsettings["ClassesDirectory"];
            containerBuilder.RegisterType<Application>();
            containerBuilder.RegisterType<ClassService>().As<IClassService>();
            containerBuilder.RegisterType<TestService>().As<ITestService>();
            containerBuilder.RegisterType<Validator<CreateClassCommand>>().As<IValidator<CreateClassCommand>>();
            containerBuilder.RegisterType<Validator<CreateMagisterClassCommand>>().As<IValidator<CreateMagisterClassCommand>>();
            containerBuilder.RegisterType<Validator<AddStudentCommand>>().As<IValidator<AddStudentCommand>>();
            containerBuilder.RegisterType<Validator<CreateTestCommand>>().As<IValidator<CreateTestCommand>>();
            containerBuilder.RegisterType<Validator<AddAssignmentCommand>>().As<IValidator<AddAssignmentCommand>>();
            containerBuilder.RegisterType<NumberOfQuestionsMustBeValid>().As<IValidationRule<AddAssignmentCommand>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.FirstNameMustBeFilled>().As<IValidationRule<AddStudentCommand>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.LastNameMustBeFilled>().As<IValidationRule<AddStudentCommand>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.TestModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateTestCommand>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.TestModelValidationRules.NumberOfVersionsMustBeValid>().As<IValidationRule<CreateTestCommand>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.ClassModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateClassCommand>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.MagisterClassModelValidationRules.FileLocationMustBeFilled>().As<IValidationRule<CreateMagisterClassCommand>>();
            var fileLocationProvider = new FileLocationProvider(
                Path.CreateFromString(classRepositoryPath),
                Path.CreateFromString(testRepositoryPath),
                Path.CreateFromString(spreadsheetDirectoryPath),
                Path.CreateFromString(magisterDirectoryPath)
                );
            containerBuilder.RegisterInstance(fileLocationProvider).AsImplementedInterfaces();

            containerBuilder.RegisterModule<AreaModule>();
            containerBuilder.RegisterModule<FileSystemModule>();
        }

        /// <summary>
        /// Adds logging to the container builder.
        /// </summary>
        internal static void RegisterLogging(ContainerBuilder containerBuilder, IConfigurationRoot configuration)
        {
            var seriLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails()
                .CreateLogger();
            var logger = new LoggerFactory()
                .AddSerilog(seriLogger)
                .CreateLogger(Assembly.GetExecutingAssembly().FullName);

            containerBuilder.RegisterInstance(logger).AsImplementedInterfaces();
        }
    }
}
