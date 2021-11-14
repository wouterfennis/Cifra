using AutoMapper;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Cifra.Api.V1.Models.Validation;
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
            CreateMap<GetAllTestsResult, GetAllTestsResponse>();
            CreateMap<Application.Models.Test.Test, V1.Models.Test.Test>();
            CreateMap<Application.Models.Test.Assignment, V1.Models.Test.Assignment>();
            CreateMap<CreateTestRequest, CreateTestCommand>();
            CreateMap<CreateTestResult, CreateTestResponse>();
            CreateMap<Application.Models.Validation.ValidationMessage, ValidationMessage>();
        }
    }
}
