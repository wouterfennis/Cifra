using AutoMapper;
using Cifra.Api.Models.Test;
using Cifra.Api.Models.Test.Results;
using Cifra.Application.Models.Test.Results;

namespace Cifra.Api.Mapping
{
    /// <summary>
    /// Automapper profile for the <see cref="Test"/> entity.
    /// </summary>
    public class TestProfile: Profile
    {
        public TestProfile()
        {
            CreateMap<GetAllTestsResult, GetAllTestsResponse>();
        }
    }
}
