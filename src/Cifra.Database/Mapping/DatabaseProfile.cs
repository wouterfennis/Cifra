using AutoMapper;
using Cifra.Application.Models.Test;
using Cifra.Application.Models.ValueTypes;

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
            CreateMap<Schema.Test, Test>();
            CreateMap<Test, Schema.Test>();
            CreateMap<StandardizationFactor, int>().ReverseMap();
            CreateMap<Grade, int>().ReverseMap();
        }
    }
}
