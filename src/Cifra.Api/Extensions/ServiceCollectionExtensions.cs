﻿using Cifra.Api.Mapping;
using Cifra.Application;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Validation;
using Cifra.Application.Validation.AssignmentModelValidationRules;
using Cifra.Database;
using Cifra.Database.Mapping;
using Cifra.FileSystem;
using Cifra.FileSystem.FileReaders;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Repositories;
using Cifra.FileSystem.Spreadsheet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpreadsheetWriter.Abstractions.File;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.EPPlus.File;
using SpreadsheetWriter.EPPlus.Formula;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Cifra.Api.Extensions
{
    /// <summary>
    /// Static class for registering dependencies.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup dependencies.
        /// </summary>
        public static void SetupDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");

            var connectionStringProvider = new ConnectionStringProvider(databaseConnectionString);
            services.AddSingleton<IConnectionStringProvider>(connectionStringProvider);

            services.AddDbContext<Context>();

            var fileLocationProvider = new FileLocationProvider(null, null, null, null);
            services.AddSingleton<IFileLocationProvider>(fileLocationProvider);

            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(DatabaseProfile)), Assembly.GetAssembly(typeof(ApiProfile)));
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IValidator<CreateClassCommand>, Validator<CreateClassCommand>>();
            services.AddScoped<IValidator<CreateMagisterClassCommand>, Validator<CreateMagisterClassCommand>>();
            services.AddScoped<IValidator<AddStudentCommand>, Validator<AddStudentCommand>>();
            services.AddScoped<IValidator<CreateTestCommand>, Validator<CreateTestCommand>>();
            services.AddScoped<IValidator<AddAssignmentCommand>, Validator<AddAssignmentCommand>>();
            services.AddScoped<IValidationRule<AddAssignmentCommand>, NumberOfQuestionsMustBeValid>();
            services.AddScoped<IValidationRule<AddStudentCommand>, Application.Validation.StudentModelValidationRules.FirstNameMustBeFilled>();
            services.AddScoped<IValidationRule<AddStudentCommand>, Application.Validation.StudentModelValidationRules.LastNameMustBeFilled>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Application.Validation.TestModelValidationRules.NameMustBeFilled>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Application.Validation.TestModelValidationRules.NumberOfVersionsMustBeValid>();
            services.AddScoped<IValidationRule<CreateMagisterClassCommand>, Application.Validation.MagisterClassModelValidationRules.FileLocationMustBeFilled>();
            services.AddScoped<IValidationRule<CreateClassCommand>, Application.Validation.ClassModelValidationRules.NameMustBeFilled>();

            services.AddScoped<ITestRepository, TestDatabaseRepository>();
            services.AddScoped<IClassRepository, ClassFileRepository>();
            services.AddScoped<IFileInfoWrapperFactory, FileInfoWrapperFactory>();
            services.AddScoped<IDirectoryInfoWrapperFactory, DirectoryInfoWrapperFactory>();
            services.AddScoped<IMagisterFileReader, MagisterFileReader>();
            services.AddScoped<ITestResultsSpreadsheetBuilder, TestResultsSpreadsheetBuilder>();
            services.AddScoped<ISpreadsheetFileFactory, ExcelFileFactory>();
            services.AddScoped<ISpreadsheetFileFactory, ExcelFileFactory>();
            services.AddScoped<IFormulaBuilderFactory, FormulaBuilderFactory>();
            services.AddScoped<IFormulaBuilder, FormulaBuilder>();
        }
    }
}


