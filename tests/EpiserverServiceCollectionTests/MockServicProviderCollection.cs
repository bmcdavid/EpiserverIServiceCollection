using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.ServiceLocation;
using StructureMap;

namespace EpiserverServiceCollectionTests
{
    public class MockServicProviderCollection : IServiceLocator, IRegisteredService
    {
        private readonly IContainer _container = new Container();

        public IRegisteredService Add(Type serviceType, Type implementationType, ServiceInstanceScope lifetime)
        {
            _container.Configure(x => { x.For(serviceType).Use(implementationType); });

            return this;
        }

        public IRegisteredService Add(Type serviceType, Func<IServiceLocator, object> implementationFactory, ServiceInstanceScope lifetime)
        {
            throw new NotImplementedException();
        }

        public IRegisteredService Add(Type serviceType, object instance)
        {
            _container.Configure(x => x.For(serviceType).Use(instance));

            return this;
        }

        public IServiceConfigurationProvider AddServiceAccessor()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).OfType<object>();
        }

        public object GetInstance(Type serviceType) => GetService(serviceType);

        public TService GetInstance<TService>() => _container.GetInstance<TService>();

        public object GetService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public IServiceConfigurationProvider RemoveAll(Type serviceType)
        {
            throw new NotImplementedException();
        }
        public bool TryGetExistingInstance(Type serviceType, out object instance)
        {
            throw new NotImplementedException();
        }
    }
}
