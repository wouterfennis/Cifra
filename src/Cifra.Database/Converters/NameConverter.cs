using Cifra.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cifra.Database
{
    internal class NameConverter : ValueConverter<Name, string>
    {
        public NameConverter() : base ((Name n) => n.Value, (string n) => Name.CreateFromString(n).Value!)
        {
        }
    }
}