using AutoMapper;
using Cifra.Application.Mapping.TypeConverters;
using Cifra.Domain;

namespace Cifra.Application.Mapping
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
            CreateMap<Database.Schema.Assignment, Assignment>().ConvertUsing<AssignmentConverter>();
            CreateMap<Assignment, Database.Schema.Assignment>();
            CreateMap<Database.Schema.Test, Test>().ConvertUsing<TestConverter>();
            CreateMap<Test, Database.Schema.Test>();

            CreateMap<Database.Schema.Student, Student>().ConvertUsing<StudentConverter>();
            CreateMap<Student, Database.Schema.Student>();
            CreateMap<Database.Schema.Class, Class>().ConvertUsing<ClassConverter>();
            CreateMap<Class, Database.Schema.Class>();
        }
    }
}
