using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Validation;
using Cifra.Application.Validation.AssignmentModelValidationRules;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Cifra.Application.Extensions
{
    /// <summary>
    /// Static class for registering dependencies.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup dependencies.
        /// </summary>
        public static void SetupApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ITestResultsSpreadsheetService, TestResultsSpreadsheetService>();
            services.AddScoped<IValidator<CreateClassCommand>, Validator<CreateClassCommand>>();
            services.AddScoped<IValidator<AddStudentCommand>, Validator<AddStudentCommand>>();
            services.AddScoped<IValidator<CreateTestCommand>, Validator<CreateTestCommand>>();
            services.AddScoped<IValidator<AddAssignmentCommand>, Validator<AddAssignmentCommand>>();
            services.AddScoped<IValidationRule<AddAssignmentCommand>, NumberOfQuestionsMustBeValid>();
            services.AddScoped<IValidationRule<AddStudentCommand>, Validation.StudentModelValidationRules.FirstNameMustBeFilled>();
            services.AddScoped<IValidationRule<AddStudentCommand>, Validation.StudentModelValidationRules.LastNameMustBeFilled>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Validation.TestModelValidationRules.NameMustBeFilled>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Validation.TestModelValidationRules.NumberOfVersionsMustBeValid>();
            services.AddScoped<IValidationRule<CreateClassCommand>, Validation.ClassModelValidationRules.NameMustBeFilled>();
        }
    }
}
