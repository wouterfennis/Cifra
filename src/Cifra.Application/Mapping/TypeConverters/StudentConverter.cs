using AutoMapper;
using Cifra.Core.Models.Class;

namespace Cifra.Application.Mapping.TypeConverters
{
    internal class StudentConverter : ITypeConverter<Database.Schema.Student, Student>
    {
        public Student Convert(Database.Schema.Student source, Student destination, ResolutionContext context)
        {
            return new Student(source.Id, source.FirstName, source.Infix, source.LastName);
        }
    }
}
