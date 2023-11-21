using Microsoft.Extensions.DependencyInjection;

namespace Cifra.Api.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCifraApiClient(this IServiceCollection services, Uri baseAddress)
        {
            services.AddHttpClient<ICifraApiClient, CifraApiClient>((client) =>
            {
                client.BaseAddress = baseAddress;
            });
            return services;
        }
    }
}
