using Cifra.Api.Client;

namespace Cifra.Api.IntegrationTests.Steps
{
    internal class ClassDetails
    {
        public required CreateClassRequest CreateClassRequest { get; init; }
        public required CreateClassResponse CreateClassResponse { get; init; }
    }
}