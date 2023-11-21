using Cifra.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cifra.Database
{
    internal class StandardizationFactorConverter : ValueConverter<StandardizationFactor, int>
    {
        public StandardizationFactorConverter() : base ((StandardizationFactor s) => s.Value, (int s) => StandardizationFactor.CreateFromInteger(s).Value!)
        {
        }
    }
}