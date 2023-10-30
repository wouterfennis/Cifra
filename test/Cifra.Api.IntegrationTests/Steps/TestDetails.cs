using Cifra.Api.Client;

namespace Cifra.Api.IntegrationTests.Steps
{
    internal class TestDetails
    {
        public required CreateTestRequest CreateTestRequest { get; init; }
        public required CreateTestResponse CreateTestResponse { get; init; }
    }
}