using AutoMapper;
using Cifra.Api.Models.Test.Requests;
using Cifra.Api.Models.Test.Responses;
using Cifra.Api.Models.Test.Results;
using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Api.V1.Test
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestController(ILogger<TestController> logger, ITestService testService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _testService = testService ?? throw new ArgumentNullException(nameof(testService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            var command = _mapper.Map<CreateTestCommand>(request);

            CreateTestResult result = await _testService.CreateTestAsync(command);

            var response = _mapper.Map<CreateTestResponse>(result);

            if (response.ValidationMessages.Any())
            {
                return BadRequest(response);
            }
            return Created(new Uri($"{response.TestId}", UriKind.Relative), response);
        }

        /// <summary>
        /// Get a list of all tests.
        /// </summary>
        /// <returns>All tests that are present</returns>
        ///<response code="200">Returns list of tests</response> 
        ///<response code="500">List could not be retrieved</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetAllTestsResponse> GetAllTestsAsync()
        {
            GetAllTestsResult getAllTestsResult = await _testService.GetTestsAsync();

            var response = _mapper.Map<GetAllTestsResponse>(getAllTestsResult);

            return response;
        }
    }
}
