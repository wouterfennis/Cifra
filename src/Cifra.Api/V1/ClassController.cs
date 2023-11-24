using Cifra.Api.Mapping;
using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Api.V1.Models.Class.Responses;
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
    /// REST endpoint for the Class resource.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IClassService _classService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClassController(ILogger<ClassController> logger, IClassService classService)
        {
            _logger = logger;
            _classService = classService;
        }

        /// <summary>
        /// Get a list of all classes.
        /// </summary>
        /// <returns>All classes that are present</returns>
        ///<response code="200">Returns list of classes</response> 
        ///<response code="500">List could not be retrieved</response> 
        [HttpGet]
        [ProducesResponseType(typeof(GetAllClassesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClassesAsync()
        {
            GetAllClassesResult getAllClassesResult = await _classService.GetClassesAsync();

            var response = getAllClassesResult.MapToResponse();

            return Ok(response);
        }

        /// <summary>
        /// Get a class.
        /// </summary>
        /// <returns>The requested class</returns>
        ///<response code="200">Returns class</response> 
        ///<response code="500">List could not be retrieved</response> 
        [HttpGet("{classId}")]
        [ProducesResponseType(typeof(GetClassResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClassAsync(uint classId)
        {
            GetClassResult getClassResult = await _classService.GetClassAsync(classId);

            var response = getClassResult.MapToResponse();

            return Ok(response);
        }

        /// <summary>
        /// Create a new Class.
        /// </summary>
        /// <returns>Reference to newly created class</returns>
        ///<response code="201">Reference to newly created class.</response> 
        ///<response code="400">Supplied class data was invalid.</response> 
        ///<response code="500">The class could not be created.</response> 
        [HttpPost]
        [ProducesResponseType(typeof(CreateClassResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateClassResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateClassAsync(CreateClassRequest request)
        {
            var command = request.MapToCommand();

            CreateClassResult result = await _classService.CreateClassAsync(command);

            var response = result.MapToResponse();

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }

            return Created(new Uri($"{response.ClassId}", UriKind.Relative), response);
        }

        /// <summary>
        /// Update a class.
        /// </summary>
        /// <param name="request">The request containing details of the class.</param>
        /// <returns>Reference to updated class</returns>
        /// <response code="200">Reference to newly created class.</response> 
        /// <response code="400">Supplied class data was invalid.</response> 
        /// <response code="500">The class could not be updated.</response> 
        [HttpPut]
        [ProducesResponseType(typeof(UpdateClassResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateClassResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClassAsync(UpdateClassRequest request)
        {
            var command = request.MapToCommand();
            UpdateClassResult result = await _classService.UpdateClassAsync(command);

            var response = result.MapToResponse();

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete a class.
        /// </summary>
        /// <param name="request">The request containing details of the class.</param>
        /// <returns>Success or failure of the request</returns>
        /// <response code="200">The class is deleted.</response> 
        /// <response code="400">Supplied class data was invalid.</response> 
        /// <response code="500">The class could not be deleted.</response> 
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteClassResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteClassResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClassAsync(DeleteClassRequest request)
        {
            var command = request.MapToCommand();
            DeleteClassResult result = await _classService.DeleteClassAsync(command);

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
