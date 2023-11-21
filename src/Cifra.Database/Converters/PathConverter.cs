using Cifra.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cifra.Database.Converters
{
    internal class PathConverter : ValueConverter<Path, string>
    {
        public PathConverter() : base((Path p) => p.Value, (string p) => Path.CreateFromString(p).Value!)
        {
        }
    }
}