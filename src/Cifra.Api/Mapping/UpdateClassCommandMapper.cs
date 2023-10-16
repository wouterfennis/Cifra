using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Application.Models;
using Cifra.Application.Models.Commands;
using System.Linq;

namespace Cifra.Api.Mapping
{
    public static class UpdateClassCommandMapper
    {
        public static UpdateClassCommand Map(this UpdateClassRequest input)
        {
            var students = input.UpdatedClass.Students
                .Select(x => new Student { Id = x.Id, FirstName = x.FirstName, Infix = x.Infix, LastName = x.LastName })
                .ToList();
            return new UpdateClassCommand
            {
                 Class = new Class { Id = input.UpdatedClass.Id, Name = input.UpdatedClass.Name, Students = students },
            };
        }
    }
}
