using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Commands;
using Cifra.Commands.Models;
using System.Linq;

namespace Cifra.Api.Mapping
{
    /// <summary>
    /// Maps a <see cref="UpdateTestRequest"/> to a <see cref="UpdateTestCommand"/>.
    /// </summary>
    public static class UpdateTestCommandMapper
    {
        /// <summary>
        /// Maps a <see cref="UpdateTestRequest"/> to a <see cref="UpdateTestCommand"/>.
        /// </summary>
        public static UpdateTestCommand MapToCommand(this UpdateTestRequest input)
        {
            var assignments = input.Test.Assignments
                .Select(x => new Assignment { Id = x.Id, NumberOfQuestions = x.NumberOfQuestions!.Value })
                .ToList();
            return new UpdateTestCommand
            {
                Test = new Test
                {
                    Id = input.Test.Id ?? 0,
                    MinimumGrade = input.Test.MinimumGrade!.Value,
                    Name = input.Test.Name!,
                    NumberOfVersions = input.Test.NumberOfVersions!.Value,
                    StandardizationFactor = input.Test.StandardizationFactor!.Value,
                    Assignments = assignments
                }
            };
        }
    }
}
