using AutoMapper;
using Cifra.Core.Models.Class;
using Cifra.Core.Models.ValueTypes;
using System.Collections.Generic;

namespace Cifra.Application.Mapping.TypeConverters
{
    internal class ClassConverter : ITypeConverter<Database.Schema.Class, Class>
    {
        public Class Convert(Database.Schema.Class source, Class destination, ResolutionContext context)
        {
            Name name = context.Mapper.Map<Name>(source.Name);
            List<Student> students = context.Mapper.Map<List<Student>>(source.Students);
            return new Class(source.Id, name, students);
        }
    }
}
