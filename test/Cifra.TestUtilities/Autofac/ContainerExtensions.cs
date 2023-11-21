using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cifra.TestUtilities.Autofac
{
    [ExcludeFromCodeCoverage] // Part of test project.
    public static class ContainerExtensions
    {
        public static IList<object> ResolveAll(this ILifetimeScope scope)
        {
            var services = scope.ComponentRegistry.Registrations.SelectMany(x => x.Services)
                .OfType<IServiceWithType>();

            foreach (var serviceWithType in services)
            {
                try
                {
                    scope.Resolve(serviceWithType.ServiceType);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }

            return services.Select(x => x.ServiceType).Select(scope.Resolve).ToList();
        }
    }
}
