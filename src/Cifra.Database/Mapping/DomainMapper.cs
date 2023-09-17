using Mapster;

namespace Cifra.Database.Mapping
{
    internal static class DomainMapper
    {
        public static Schema.Class MapToSchema(this Domain.Class input)
        {
            return input.Adapt<Schema.Class>();
        }

        public static Schema.Test MapToSchema(this Domain.Test input)
        {
            return input.Adapt<Schema.Test>();
        }
    }
}
