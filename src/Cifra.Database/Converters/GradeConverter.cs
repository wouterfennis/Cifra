using Cifra.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cifra.Database.Converters
{
    internal class GradeConverter : ValueConverter<Grade, int>
    {
        public GradeConverter() : base((g) => g.Value, (g) => Grade.CreateFromInteger(g).Value!)
        {
        }
    }
}