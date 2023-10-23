using Cifra.Api.Mapping;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Cifra.Application;
using Cifra.Application.Models.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Api.V1
{
    /// <summary>
    /// REST endpoint for the Test resource.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITestService _testService;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestController(ILogger<TestController> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        /// <summary>
        /// Get a list of all tests.
        /// </summary>
        /// <returns>All tests that are present</returns>
        ///<response code="200">Returns list of tests</response> 
        ///<response code="500">List could not be retrieved</response> 
        [HttpGet]
        [ProducesResponseType(typeof(GetAllTestsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetAllTestsResponse> GetAllTestsAsync()
        {
            GetAllTestsResult getAllTestsResult = await _testService.GetTestsAsync();

            var response = getAllTestsResult.MapToResponse();

            return response;
        }

        /// <summary>
        /// Get a test.
        /// </summary>
        /// <returns>The requested test</returns>
        ///<response code="200">Returns test</response> 
        ///<response code="500">Test could not be retrieved</response> 
        [HttpGet("{testId}")]
        [ProducesResponseType(typeof(GetTestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetTestResponse> GetTestAsync(uint testId)
        {
            GetTestResult getTestResult = await _testService.GetTestAsync(testId);

            var response = getTestResult.MapToResponse();

            return response;
        }

        /// <summary>
        /// Create a new Test.
        /// </summary>
        /// <returns>Reference to newly created test</returns>
        ///<response code="201">Reference to newly created test.</response> 
        ///<response code="400">Supplied test data was invalid.</response> 
        ///<response code="500">The test could not be created.</response> 
        [HttpPost]
        [ProducesResponseType(typeof(CreateTestResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateTestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTestAsync(CreateTestRequest request)
        {
            var command = request.MapToCommand();

            CreateTestResult result = await _testService.CreateTestAsync(command);

            var response = result.MapToResponse();

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }
            return Created(new Uri($"{response.TestId}", UriKind.Relative), response);
        }

        /// <summary>
        /// Update a test.
        /// </summary>
        /// <param name="request">The request containing details of the test.</param>
        /// <response code="201">Reference to updated test.</response> 
        /// <response code="400">Supplied test data was invalid.</response> 
        /// <response code="500">The test could not be updated.</response> 
        [HttpPut]
        [ProducesResponseType(typeof(UpdateTestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateTestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTestAsync(UpdateTestRequest request)
        {
            var command = request.MapToCommand();
            UpdateTestResult result = await _testService.UpdateTestAsync(command);

            var response = result.MapToResponse();

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Delete a test.
        /// </summary>
        /// <param name="request">The request containing details of the test.</param>
        /// <response code="201">Reference to updated test.</response> 
        /// <response code="400">Supplied test data was invalid.</response> 
        /// <response code="500">The test could not be deleted.</response> 
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteTestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteTestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTestAsync(DeleteTestRequest request)
        {
            var command = request.MapToCommand();
            DeleteTestResult result = await _testService.DeleteTestAsync(command);

            var response = result.MapToResponse();

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
