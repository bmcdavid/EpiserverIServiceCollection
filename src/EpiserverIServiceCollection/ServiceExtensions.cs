using System;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;

namespace EpiserverIServiceCollection
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds ServiceDescriptor to Episerver services
        /// </summary>
        /// <param name="serviceConfiguration"></param>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceConfigurationProvider AddServiceCollection(this IServiceConfigurationProvider serviceConfiguration, IServiceCollection serviceCollection)
        {
            foreach (var service in serviceCollection)
            {
                if (service.ImplementationInstance != null)
                {
                    serviceConfiguration.Add(service.ServiceType, service.ImplementationInstance);
                }
                else if (service.ImplementationFactory != null)
                {
                    serviceConfiguration.Add
                    (
                        service.ServiceType,
                        (locator) => locator.GetService(service.ServiceType),
                        ConvertLifeTime(service.Lifetime)
                    );
                }
                else if (service.ImplementationType != null)
                {
                    serviceConfiguration.Add
                    (
                        service.ServiceType,
                        service.ImplementationType,
                        ConvertLifeTime(service.Lifetime)
                    );
                }
            }

            return serviceConfiguration;
        }

        /// <summary>
        /// Converts dependency injection ServiceLifetime to Episerver ServiceInstanceScope
        /// </summary>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static ServiceInstanceScope ConvertLifeTime(ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    return ServiceInstanceScope.Hybrid;
                case ServiceLifetime.Singleton:
                    return ServiceInstanceScope.Singleton;
                case ServiceLifetime.Transient:
                    return ServiceInstanceScope.Transient;
                default:
                    throw new ArgumentException($"Cannot convert {lifetime} to a {typeof(ServiceLifetime).FullName} type!");
            }
        }
    }
}