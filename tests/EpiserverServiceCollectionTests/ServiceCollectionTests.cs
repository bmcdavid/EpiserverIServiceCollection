using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EpiserverIServiceCollection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EpiserverServiceCollectionTests
{
    [TestClass]
    public class ServiceCollectionTests
    {
        [TestMethod]
        public void ShouldAddServiceUsingCallback()
        {
            //Example that would be added using callback in a 
            // [assembly: PreApplicationStartMethod(typeof(InitClass), nameof(StaticVoidInitFunction))]
            MapServiceCollection.ConfigureServices += (serviceCollection) =>
            {
                serviceCollection.AddTransient<IFoo, Foo>();
            };

            var provider = new MockServicProviderCollection();
            var context = new ServiceConfigurationContext(HostType.TestFramework, provider);
            IConfigurableModule module = new MapServiceCollection();
            module.ConfigureContainer(context);

            Assert.IsTrue(provider.GetService<IFoo>().Enabled);
        }
    }
}