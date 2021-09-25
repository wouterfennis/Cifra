using Cifra.Api.Models.Test.Results;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cifra.Api.Controllers
{
    /// <summary>
    /// REST endpoint for the Test resource.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITestService _testService;

        public TestController(ILogger<TestController> logger, ITestService testService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        /// <summary>
        /// Get a list of all tests.
        /// </summary>
        /// <returns>All tests that are present</returns>
        [HttpGet]
        public async Task<GetAllTestsResponse> GetAllTestsAsync()
        {
            GetAllTestsResult getAllTestsResult = await _testService.GetTestsAsync();

            // map

            return new GetAllTestsResponse(null);
        }
    }
}
