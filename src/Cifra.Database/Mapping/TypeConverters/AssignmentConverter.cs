using AutoMapper;
using Cifra.Application.Models.Test;

namespace Cifra.Database.Mapping.TypeConverters
{
    class AssignmentConverter : ITypeConverter<Schema.Assignment, Assignment>, ITypeConverter<Assignment, Schema.Assignment>
    {
        public Assignment Convert(Schema.Assignment source, Assignment destination, ResolutionContext context)
        {
            return new Assignment(source.Id, source.NumberOfQuestions);
        }

        public Schema.Assignment Convert(Assignment source, Schema.Assignment destination, ResolutionContext context)
        {
            return new Schema.Assignment
            {
                Id = source.Id,
                NumberOfQuestions = source.NumberOfQuestions
            };
        }
    }
}
