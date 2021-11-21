using AutoMapper;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using System.Collections.Generic;

namespace Cifra.Database.Mapping.TypeConverters
{
    internal class ClassConverter : ITypeConverter<Schema.Class, Class>
    {
        public Class Convert(Schema.Class source, Class destination, ResolutionContext context)
        {
            Name name = context.Mapper.Map<Name>(source.Name);
            List<Student> students = context.Mapper.Map<List<Student>>(source.Students);
            return new Class(source.Id, name, students);
        }
    }
}
