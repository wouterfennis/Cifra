using Cifra.Api.V1.Models.Test;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Cifra.Api.IntegrationTests.Api.V1
{
    public class ApiClient : IApiClient
    {
        private readonly IConfigurationSection _apiConfiguration;
        private readonly RestClient _restClient;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApiClient(IConfigurationSection apiConfiguration)
        {
            _apiConfiguration = apiConfiguration ?? throw new ArgumentNullException(nameof(apiConfiguration));
            string testApiUrl = _apiConfiguration.GetSection("Test").Value;
            _restClient = new RestClient(testApiUrl);
        }

        /// <summary>
        /// GET on the Test resource
        /// </summary>
        public async Task<GetAllTestsResponse> GetAllTestsAsync()
        {
            var request = new RestRequest(Method.GET);

            return await _restClient.GetAsync<GetAllTestsResponse>(request);
        }

        /// <summary>
        /// POST on the Test resource
        /// </summary>
        public async Task<CreateTestResponse> CreateTestAsync(CreateTestRequest createTestRequest)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(createTestRequest);

            return await _restClient.PostAsync<CreateTestResponse>(request);
        }
    }
}
