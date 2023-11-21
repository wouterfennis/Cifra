using AutoFixture;
using Cifra.Api.Client;
using System;

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
            return new CreateTestRequest
            {
                Name = _fixture.Create<string>(),
                MinimumGrade = Random.Shared.Next(1, 10),
                NumberOfVersions = Random.Shared.Next(1, 3),
                StandardizationFactor = _fixture.Create<int>()
            };
        }
    }
}
