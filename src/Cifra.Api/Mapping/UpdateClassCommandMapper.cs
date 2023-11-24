using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Commands;
using Cifra.Commands.Models;
using System.Linq;

namespace Cifra.Api.Mapping
{
    /// <summary>
    /// Maps a <see cref="CreateClassRequest"/> to a <see cref="CreateClassCommand"/>.
    /// </summary>
    public static class UpdateClassCommandMapper
    {
        /// <summary>
        /// Maps a <see cref="CreateClassRequest"/> to a <see cref="CreateClassCommand"/>.
        /// </summary>
        public static UpdateClassCommand MapToCommand(this UpdateClassRequest input)
        {
            var students = input.UpdatedClass.Students
                .Select(x => new Student
                {
                    Id = x.Id ?? 0,
                    FirstName = x.FirstName!,
                    Infix = x.Infix,
                    LastName = x.LastName!
                })
                .ToList();

            return new UpdateClassCommand
            {
                Class = new Class
                {
                    Id = input.UpdatedClass.Id ?? 0,
                    Name = input.UpdatedClass.Name!,
                    Students = students
                }
            };
        }
    }
}
