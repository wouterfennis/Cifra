using AutoMapper;
using Cifra.Application.Models.ValueTypes;

namespace Cifra.Database.Mapping.TypeConverters
{
    /// <summary>
    /// Converter to convert <see cref="int"/> to <see cref="StandardizationFactor"/> and reverse.
    /// </summary>
    public class StandardizationFactorConverter : ITypeConverter<int, StandardizationFactor>, ITypeConverter<StandardizationFactor, int>
    {
        /// <inheritdoc/>
        public int Convert(StandardizationFactor source, int destination, ResolutionContext context)
        {
            return source.Value;
        }

        /// <inheritdoc/>
        public StandardizationFactor Convert(int source, StandardizationFactor destination, ResolutionContext context)
        {
            return StandardizationFactor.CreateFromInteger(source);
        }
    }
}
