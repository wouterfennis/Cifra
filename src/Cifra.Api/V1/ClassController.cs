using AutoMapper;
using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Api.V1.Models.Class.Responses;
using Cifra.Api.V1.Models.Validation;
using Cifra.Application;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifra.Api.V1
{
    /// <summary>
    /// REST endpoint for the Class resource.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClassController(ILogger<ClassController> logger, IClassService classService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _classService = classService ?? throw new ArgumentNullException(nameof(classService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
        public async Task<GetAllClassesResponse> GetAllClassesAsync()
        {
            GetAllClassesResult getAllClassesResult = await _classService.GetClassesAsync();

            var response = _mapper.Map<GetAllClassesResponse>(getAllClassesResult);

            return response;
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
            var command = _mapper.Map<CreateClassCommand>(request);

            CreateClassResult result = await _classService.CreateClassAsync(command);

            var response = _mapper.Map<CreateClassResponse>(result);

            if (response.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                return BadRequest(response);
            }
            return Created(new Uri($"{response.ClassId}", UriKind.Relative), response);
        }

        /// <summary>
        /// Adds an student to a class.
        /// </summary>
        /// <param name="classId">The class id where the student should be added.</param>
        /// <param name="request">The request containing details of the student.</param>
        /// <returns>Reference to newly created student</returns>
        /// <response code="201">Reference to newly created student.</response> 
        /// <response code="400">Supplied student data was invalid.</response> 
        /// <response code="500">The student could not be created.</response> 
        [HttpPost]
        [Route("{classId}/Student")]
        [ProducesResponseType(typeof(AddStudentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AddStudentResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddStudentsAsync(int classId, AddStudentsRequest request)
        {
            var validationMessages = new List<ValidationMessage>();
            foreach (var student in request.Students)
            {
                var command = new AddStudentCommand
                {
                    ClassId = classId,
                    FirstName = student.FirstName,
                    Infix = student.Infix,
                    LastName = student.LastName
                };

                AddStudentResult result = await _classService.AddStudentAsync(command);

                var response = _mapper.Map<AddStudentResponse>(result);

                if (response.ValidationMessages.Any())
                {
                    _logger.LogInformation("Request is not valid");
                    validationMessages.AddRange(response.ValidationMessages);
                }
            }
            
            return Created(new Uri($"", UriKind.Relative), validationMessages);
        }
    }
}
