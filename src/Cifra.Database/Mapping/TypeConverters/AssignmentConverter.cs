using AutoMapper;
using Cifra.Application.Models.Test;

namespace Cifra.Database.Mapping.TypeConverters
{
    class AssignmentConverter : ITypeConverter<Schema.Assignment, Assignment>
    {
        public Assignment Convert(Schema.Assignment source, Assignment destination, ResolutionContext context)
        {
            return new Assignment(source.Id, source.NumberOfQuestions);
        }
    }
}
