using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Validation;
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
            services.AddScoped<IValidator<CreateTestCommand>, Validator<CreateTestCommand>>();
            services.AddScoped<IValidator<UpdateTestCommand>, Validator<UpdateTestCommand>>();
            services.AddScoped<IValidator<UpdateClassCommand>, Validator<UpdateClassCommand>>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Validation.CreateTestModelValidationRules.NameMustBeFilled>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Validation.CreateTestModelValidationRules.NumberOfVersionsMustBeValid>();
            services.AddScoped<IValidationRule<CreateTestCommand>, Validation.CreateTestModelValidationRules.MinimumGradeMustBeValid>();
            services.AddScoped<IValidationRule<UpdateTestCommand>, Validation.UpdateTestModelValidationRules.NameMustBeFilled>();
            services.AddScoped<IValidationRule<UpdateTestCommand>, Validation.UpdateTestModelValidationRules.NumberOfVersionsMustBeValid>();
            services.AddScoped<IValidationRule<UpdateTestCommand>, Validation.UpdateTestModelValidationRules.MinimumGradeMustBeValid>();
            services.AddScoped<IValidationRule<CreateClassCommand>, Validation.CreateClassModelValidationRules.NameMustBeFilled>();
            services.AddScoped<IValidationRule<UpdateClassCommand>, Validation.UpdateClassModelValidationRules.NameMustBeFilled>();
        }
    }
}
