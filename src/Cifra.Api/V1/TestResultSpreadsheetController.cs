using Cifra.Api.Mapping;
using Cifra.Api.V1.Models.Spreadsheet.Requests;
using Cifra.Application;
using Cifra.Application.Models.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
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
    public class TestResultSpreadsheetController : ControllerBase
    {
        private readonly ILogger<TestResultSpreadsheetController> _logger;
        private readonly ITestResultsSpreadsheetService _testResultsSpreadsheetService;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestResultSpreadsheetController(ILogger<TestResultSpreadsheetController> logger, 
            ITestResultsSpreadsheetService testResultsSpreadsheetService)
        {
            _logger = logger;
            _testResultsSpreadsheetService = testResultsSpreadsheetService;
        }

        /// <summary>
        /// Create a new test results spreadsheet.
        /// </summary>
        /// <returns>Reference to newly created test results spreadsheet</returns>
        ///<response code="201">Reference to newly created test results spreadsheet.</response> 
        ///<response code="400">Supplied test results spreadsheet data was invalid.</response> 
        ///<response code="500">The test results spreadsheet could not be created.</response> 
        [HttpPost]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTestResultsSpreadsheetAsync(CreateTestResultsSpreadsheetRequest request)
        {
            var command = request.MapToCommand();

            CreateTestResultsSpreadsheetResult result = await _testResultsSpreadsheetService.CreateTestResultsSpreadsheetAsync(command);

            if (result.ValidationMessages.Any())
            {
                _logger.LogInformation("Request is not valid");
                _logger.LogInformation(result.ValidationMessages.Select( x=> x.Message).First());
                return BadRequest(result);
            }
            return base.File(CreateOneTimeStream(result), "application/octet-stream", result.FileInfo.Name);
        }

        private static FileStream CreateOneTimeStream(CreateTestResultsSpreadsheetResult result)
        {
            var fileStreamOptions = new FileStreamOptions
            {
                Access = FileAccess.Read,
                Mode = FileMode.Open,
                Options = FileOptions.DeleteOnClose,
            };
            return result.FileInfo.Open(fileStreamOptions);
        }
    }
}
