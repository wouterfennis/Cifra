using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;

namespace Cifra.Api.IntegrationTests.Models
{
    internal class TestDetails
    {
        public CreateTestRequest CreateTestRequest { get; set; }
        public CreateTestResponse CreateTestResponse { get; set; }
    }
}
