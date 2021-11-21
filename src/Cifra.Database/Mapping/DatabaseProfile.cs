using AutoMapper;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;
using Cifra.Database.Mapping.TypeConverters;

namespace Cifra.Database.Mapping
{
    /// <summary>
    /// Automapper profile for the <see cref="Database"/> assembly.
    /// </summary>
    public class DatabaseProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseProfile()
        {
            CreateMap<Schema.Assignment, Assignment>().ConvertUsing<AssignmentConverter>();
            CreateMap<Assignment, Schema.Assignment>();
            CreateMap<Schema.Test, Test>().ConvertUsing<TestConverter>();
            CreateMap<Test, Schema.Test>();

            CreateMap<Schema.Student, Student>().ConvertUsing<StudentConverter>();
            CreateMap<Student, Schema.Student>();
            CreateMap<Schema.Class, Class>().ConvertUsing<ClassConverter>();
            CreateMap<Class, Schema.Class>();
        }
    }
}
