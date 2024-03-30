using Cifra.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cifra.Database.Converters
{
    internal class GradeConverter : ValueConverter<Grade, decimal>
    {
        public GradeConverter() : base((g) => g.Value, (g) => Grade.CreateFromDecimal(g).Value!)
        {
        }
    }
}