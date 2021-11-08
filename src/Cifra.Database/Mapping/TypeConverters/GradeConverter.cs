using AutoMapper;
using Cifra.Application.Models.ValueTypes;

namespace Cifra.Database.Mapping.TypeConverters
{
    /// <summary>
    /// Converter to convert <see cref="int"/> to <see cref="Grade"/> and reverse.
    /// </summary>
    public class GradeConverter : ITypeConverter<int, Grade>, ITypeConverter<Grade, int>
    {
        /// <inheritdoc/>
        public int Convert(Grade source, int destination, ResolutionContext context)
        {
            return source.Value;
        }

        /// <inheritdoc/>
        public Grade Convert(int source, Grade destination, ResolutionContext context)
        {
            return Grade.CreateFromInteger(source);
        }
    }
}
