using AutoMapper;
using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Api.V1.Models.Class.Responses;
using Cifra.Api.V1.Models.Spreadsheet.Requests;
using Cifra.Api.V1.Models.Spreadsheet.Responses;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Cifra.Api.V1.Models.Validation;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Spreadsheet.Commands;
using Cifra.Application.Models.Spreadsheet.Results;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;

namespace Cifra.Api.Mapping
{
    /// <summary>
    /// Automapper profile for the <see cref="Api"/> assembly.
    /// </summary>
    public class ApiProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ApiProfile()
        {
            // Test
            CreateMap<GetAllTestsResult, GetAllTestsResponse>();
            CreateMap<Core.Models.Test.Test, V1.Models.Test.Test>();
            CreateMap<Core.Models.Test.Assignment, V1.Models.Test.Assignment>();
            CreateMap<CreateTestRequest, CreateTestCommand>();
            CreateMap<CreateTestResult, CreateTestResponse>();
            CreateMap<Core.Models.Validation.ValidationMessage, ValidationMessage>();

            CreateMap<AddAssignmentResult, AddAssignmentResponse>();

            // Class
            CreateMap<GetAllClassesResult, GetAllClassesResponse>();
            CreateMap<GetClassResult, GetClassResponse>();
            CreateMap<Core.Models.Class.Class, V1.Models.Class.Class>();
            CreateMap<Core.Models.Class.Student, V1.Models.Class.Student>();
            CreateMap<CreateClassRequest, CreateClassCommand>();
            CreateMap<CreateClassResult, CreateClassResponse>();

            CreateMap<AddStudentResult, AddStudentResponse>();

            // Spreadsheet
            CreateMap<CreateTestResultsSpreadsheetRequest, CreateTestResultsSpreadsheetCommand>();
            CreateMap<CreateTestResultsSpreadsheetResult, CreateTestResultsSpreadsheetResponse>();
        }
    }
}
