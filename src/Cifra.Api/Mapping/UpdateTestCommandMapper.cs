using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Application.Models.Test.Commands;
using System.Linq;

namespace Cifra.Api.Mapping
{
    public static class UpdateTestCommandMapper
    {
        public static UpdateTestCommand MapToCommand(this UpdateTestRequest input)
        {
            var assignments = input.Test.Assignments
                .Select(x => new Domain.Assignment(x.Id, x.NumberOfQuestions))
                .ToList();
            return new UpdateTestCommand
            {
                Test = new Domain.Test(input.Test.Id, input.Test.Name, input.Test.StandardizationFactor, input.Test.MinimumGrade, assignments, input.Test.NumberOfVersions),
            };
        }
    }
}
