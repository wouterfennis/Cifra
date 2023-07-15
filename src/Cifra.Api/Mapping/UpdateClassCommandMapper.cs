using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Application.Models.Class.Commands;
using System.Linq;

namespace Cifra.Api.Mapping
{
    public static class UpdateClassCommandMapper
    {
        public static UpdateClassCommand Map(this UpdateClassRequest input)
        {
            var students = input.UpdatedClass.Students
                .Select(x => new Domain.Student(x.Id, x.FirstName, x.Infix, x.LastName))
                .ToList();
            return new UpdateClassCommand
            {
                UpdatedClass = new Domain.Class(input.UpdatedClass.Id, input.UpdatedClass.Name, students),
            };
        }
    }
}
