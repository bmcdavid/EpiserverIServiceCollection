using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EpiserverIServiceCollection
{
    /// <summary>
    /// Run after ServiceConfigurationAttribute assignments
    /// </summary>
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class MapServiceCollection : IConfigurableModule
    {
        /// <summary>
        /// Service configuration callback, must use a PreApplicationStartMethod assembly attribute to utilize for ASP.Net Framework projects
        /// </summary>
        public static Action<IServiceCollection> ConfigureServices { get; set; }

        void IConfigurableModule.ConfigureContainer(ServiceConfigurationContext context)
        {
            var serviceCollection = new ServiceCollection();            

            //todo: manually add services or
            //example: serviceCollection.AddTransient<IFoo, Foo>();

            // use the static callback
            ConfigureServices?.Invoke(serviceCollection);
            context.Services.AddServiceCollection(serviceCollection);
        }

        void IInitializableModule.Initialize(InitializationEngine context)
        {
        }

        void IInitializableModule.Uninitialize(InitializationEngine context)
        {
        }
    }
}