﻿using BoDi;
using Cifra.Api.Client;
using Cifra.Database;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Cifra.Api.IntegrationTests
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void Initialize()
        {
            var httpClient = _objectContainer.Resolve<CifraApiWebApplicationFactory>().CreateClient();
            var cifraApiClient = new CifraApiClient(httpClient);
            _objectContainer.RegisterInstanceAs<ICifraApiClient>(cifraApiClient);

            EnsureDatabaseReady();
        }

        private void EnsureDatabaseReady()
        {
            var services = _objectContainer.Resolve<CifraApiWebApplicationFactory>().Services;
            using var scope = services.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<Context>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
