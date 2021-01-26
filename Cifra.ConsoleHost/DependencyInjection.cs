﻿using Autofac;
using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Test.Requests;
using Cifra.Application.Models.ValueTypes;
using Cifra.Application.Validation;
using Cifra.Application.Validation.QuestionModelValidationRules;
using Cifra.FileSystem;
using Microsoft.Extensions.Configuration;

namespace Cifra.ConsoleHost
{
    internal class DependencyInjection
    {
        internal static IContainer RegisterDependencies(IConfigurationSection appsettings)
        {
            string classRepositoryPath = appsettings["ClassRepository"];
            string testRepositoryPath = appsettings["TestRepository"];
            string spreadsheetDirectoryPath = appsettings["SpreadsheetDirectory"];
            string magisterDirectoryPath = appsettings["MagisterDirectory"];
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<ClassService>().As<IClassService>();
            builder.RegisterType<TestService>().As<ITestService>();

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
            builder.RegisterModule<FileSystemModule>();

            return builder.Build();
        }
    }
}