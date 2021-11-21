using AutoMapper;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;

namespace Cifra.Database.Mapping.TypeConverters
{
    internal class StudentConverter : ITypeConverter<Schema.Student, Student>
    {
        public Student Convert(Schema.Student source, Student destination, ResolutionContext context)
        {
            return new Student(source.Id, source.FirstName, source.Infix, source.LastName);
        }
    }
}
