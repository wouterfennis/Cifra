using AutoMapper;
using Cifra.Application.Models.ValueTypes;

namespace Cifra.Database.Mapping.TypeConverters
{
    /// <summary>
    /// Converter to convert <see cref="int"/> to <see cref="Name"/> and reverse.
    /// </summary>
    public class NameConverter : ITypeConverter<string, Name>, ITypeConverter<Name, string>
    {
        /// <inheritdoc/>
        public string Convert(Name source, string destination, ResolutionContext context)
        {
            return source.Value;
        }

        /// <inheritdoc/>
        public Name Convert(string source, Name destination, ResolutionContext context)
        {
            return Name.CreateFromString(source);
        }
    }
}
