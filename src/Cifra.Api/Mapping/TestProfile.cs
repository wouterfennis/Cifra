using AutoMapper;
using Cifra.Api.Models.Test;
using Cifra.Api.Models.Test.Requests;
using Cifra.Api.Models.Test.Responses;
using Cifra.Api.Models.Test.Results;
using Cifra.Api.Models.Validation;
using Cifra.Application.Models.Test.Commands;
using Cifra.Application.Models.Test.Results;

namespace Cifra.Api.Mapping
{
    /// <summary>
    /// Automapper profile for the <see cref="Test"/> entity.
    /// </summary>
    public class TestProfile: Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TestProfile()
        {
            CreateMap<GetAllTestsResult, GetAllTestsResponse>();
            CreateMap<CreateTestRequest, CreateTestCommand>();
            CreateMap<CreateTestResult, CreateTestResponse>();
            CreateMap<Application.Models.Validation.ValidationMessage, ValidationMessage>();
        }
    }
}
