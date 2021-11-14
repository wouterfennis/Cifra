using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;

namespace Cifra.Api.IntegrationTests.Api.V1
{
    public interface IApiClient
    {
        Task<CreateTestResponse> CreateTestAsync(CreateTestRequest createTestRequest);
        Task<GetAllTestsResponse> GetAllTestsAsync();
    }
}