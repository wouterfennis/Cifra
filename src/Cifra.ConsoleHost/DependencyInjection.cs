using Autofac;
using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using Cifra.Application.Validation.AssignmentModelValidationRules;
using Cifra.FileSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

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
            containerBuilder.RegisterType<Validator<CreateClassRequest>>().As<IValidator<CreateClassRequest>>();
            containerBuilder.RegisterType<Validator<CreateMagisterClassRequest>>().As<IValidator<CreateMagisterClassRequest>>();
            containerBuilder.RegisterType<Validator<AddStudentRequest>>().As<IValidator<AddStudentRequest>>();
            containerBuilder.RegisterType<Validator<CreateTestRequest>>().As<IValidator<CreateTestRequest>>();
            containerBuilder.RegisterType<Validator<AddAssignmentRequest>>().As<IValidator<AddAssignmentRequest>>();
            containerBuilder.RegisterType<NumberOfQuestionsMustBeValid>().As<IValidationRule<AddAssignmentRequest>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.FirstNameMustBeFilled>().As<IValidationRule<AddStudentRequest>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.StudentModelValidationRules.LastNameMustBeFilled>().As<IValidationRule<AddStudentRequest>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.TestModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateTestRequest>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.TestModelValidationRules.NumberOfVersionsMustBeValid>().As<IValidationRule<CreateTestRequest>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.ClassModelValidationRules.NameMustBeFilled>().As<IValidationRule<CreateClassRequest>>();
            containerBuilder.RegisterType<Cifra.Application.Validation.MagisterClassModelValidationRules.FileLocationMustBeFilled>().As<IValidationRule<CreateMagisterClassRequest>>();
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
                .CreateLogger();
            var logger = new LoggerFactory()
                .AddSerilog(seriLogger)
                .CreateLogger("");

            containerBuilder.RegisterInstance(logger).AsImplementedInterfaces();
        }
    }
}
