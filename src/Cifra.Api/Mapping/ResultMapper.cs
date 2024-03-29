﻿using Cifra.Api.V1.Models.Class.Responses;
using Cifra.Api.V1.Models.Test.Responses;
using Cifra.Api.V1.Models.Test.Results;
using Cifra.Application.Models.Results;
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

        public static DeleteClassResponse MapToResponse(this DeleteClassResult input)
        {
            return input.Adapt<DeleteClassResponse>();
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

        public static DeleteTestResponse MapToResponse(this DeleteTestResult input)
        {
            return input.Adapt<DeleteTestResponse>();
        }
    }
}
