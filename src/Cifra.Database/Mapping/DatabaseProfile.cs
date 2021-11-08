using AutoMapper;
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
            CreateMap<Assignment, Schema.Assignment>().ConvertUsing<AssignmentConverter>();
            CreateMap<Schema.Test, Test>().ConvertUsing<TestConverter>();
            CreateMap<Test, Schema.Test>().ConvertUsing<TestConverter>();
            CreateMap<StandardizationFactor, int>().ConvertUsing<StandardizationFactorConverter>();
            CreateMap<int, StandardizationFactor>().ConvertUsing<StandardizationFactorConverter>();
            CreateMap<Grade, int>().ConvertUsing<GradeConverter>();
            CreateMap<int, Grade>().ConvertUsing<GradeConverter>();
            CreateMap<Name, string>().ConvertUsing<NameConverter>();
            CreateMap<string, Name>().ConvertUsing<NameConverter>();
        }
    }
}
