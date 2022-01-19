using AutoMapper;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Cifra.Application;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;
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

            var response = _mapper.Map<GetAllTestsResponse>(getAllTestsResult);

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
            var command = _mapper.Map<CreateTestCommand>(request);

            CreateTestResult result = await _testService.CreateTestAsync(command);

            var response = _mapper.Map<CreateTestResponse>(result);

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }
            return Created(new Uri($"{response.TestId}", UriKind.Relative), response);
        }

        /// <summary>
        /// Adds an assignment to a test.
        /// </summary>
        /// <param name="testId">The test id where the assignment should be added.</param>
        /// <param name="request">The request containing details of the assignment.</param>
        /// <returns>Reference to newly created assignment</returns>
        /// <response code="201">Reference to newly created assignment.</response> 
        /// <response code="400">Supplied assignment data was invalid.</response> 
        /// <response code="500">The assignment could not be created.</response> 
        [HttpPost]
        [Route("{testId}/Assignment")]
        [ProducesResponseType(typeof(AddAssignmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AddAssignmentResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddAssignmentAsync(int testId, AddAssignmentRequest request)
        {
            var command = new AddAssignmentCommand
            {
                TestId = testId,
                NumberOfQuestions = request.NumberOfQuestions
            };

            AddAssignmentResult result = await _testService.AddAssignmentAsync(command);

            var response = _mapper.Map<AddAssignmentResponse>(result);

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }
            return Created(new Uri($"{response.TestId}", UriKind.Relative), response);
        }
    }
}
