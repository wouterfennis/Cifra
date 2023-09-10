using AutoFixture;
using Cifra.Api.Client;

namespace Cifra.Api.IntegrationTests.Builders
{
    public sealed class TestBuilder
    {
        private readonly Fixture _fixture;

        public TestBuilder()
        {
            _fixture = new Fixture();
        }

        public CreateTestRequest BuildRandomCreateTestRequest()
        {
            //var rangedNumberGenerator = new RangedNumberGenerator();
            //var rangeNumberRequest = new RangedNumberRequest(typeof(int), 1, 10);
            //return new CreateTestRequest { 
            //Name = _fixture.Create<string>(),
            //MinimumGrade = rangedNumberGenerator.Create(rangeNumberRequest),
            //NumberOfVersions = _fixture.Create<int>(),
            //StandardizationFactor = _fixture.Create<int>()
            //};

            return _fixture.Create<CreateTestRequest>();
        }
    }
}
