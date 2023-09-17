using Cifra.Api.V1.Models.Class.Responses;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Cifra.Application.Models.Class.Results;
using Cifra.Application.Models.Test.Results;
using Mapster;

namespace Cifra.Api.Mapping
{
    internal static class ResultMapper
    {
        public static GetAllClassesResponse MapToResponse(this GetAllClassesResult input)
        {
            return input.Adapt<GetAllClassesResponse>();
        }

        public static GetClassResponse MapToResponse(this GetClassResult input)
        {
            return input.Adapt<GetClassResponse>();
        }

        public static CreateClassResponse MapToResponse(this CreateClassResult input)
        {
            return input.Adapt<CreateClassResponse>();
        }

        public static UpdateClassResponse MapToResponse(this UpdateClassResult input)
        {
            return input.Adapt<UpdateClassResponse>();
        }

        public static GetAllTestsResponse MapToResponse(this GetAllTestsResult input)
        {
            return input.Adapt<GetAllTestsResponse>();
        }

        public static GetTestResponse MapToResponse(this GetTestResult input)
        {
            return input.Adapt<GetTestResponse>();
        }

        public static CreateTestResponse MapToResponse(this CreateTestResult input)
        {
            return input.Adapt<CreateTestResponse>();
        }

        public static UpdateTestResponse MapToResponse(this UpdateTestResult input)
        {
            return input.Adapt<UpdateTestResponse>();
        }
    }
}
