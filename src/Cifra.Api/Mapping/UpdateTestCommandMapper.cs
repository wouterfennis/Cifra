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
                .Select(x => new Assignment { Id = x.Id, NumberOfQuestions = x.NumberOfQuestions })
                .ToList();
            return new UpdateTestCommand
            {
                Test = new Test
                {
                    Id = input.Test.Id,
                    MinimumGrade = input.Test.MinimumGrade,
                    Name = input.Test.Name,
                    NumberOfVersions = input.Test.NumberOfVersions,
                    StandardizationFactor = input.Test.StandardizationFactor,
                    Assignments = assignments
                }
            };
        }
    }
}
