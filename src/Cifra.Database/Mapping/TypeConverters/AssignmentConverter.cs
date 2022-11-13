using AutoMapper;
using Cifra.Domain;

namespace Cifra.Application.Mapping.TypeConverters
{
    internal class AssignmentConverter : ITypeConverter<Database.Schema.Assignment, Assignment>
    {
        public Assignment Convert(Database.Schema.Assignment source, Assignment destination, ResolutionContext context)
        {
            return new Assignment(source.Id, source.NumberOfQuestions);
        }
    }
}
