using AutoFixture;
using Cifra.Api.Client;

namespace Cifra.Api.IntegrationTests.Builders
{
    public sealed class ClassBuilder
    {
        private readonly Fixture _fixture;

        public ClassBuilder()
        {
            _fixture = new Fixture();
        }

        public CreateClassRequest BuildRandomCreateClassRequest()
        {
            return new CreateClassRequest
            {
                Name = _fixture.Create<string>()
            };
        }
    }
}
