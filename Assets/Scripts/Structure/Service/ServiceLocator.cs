using System;
using System.Collections.Generic;
using System.Linq;

namespace Structure.Service
{
    public class ServiceLocator : IServiceLocator
    {
        public static IServiceLocator Instance { get; private set; }

        private readonly Dictionary<Type, List<object>> _services;

        public ServiceLocator()
        {
            Instance = this;
            _services = new Dictionary<Type, List<object>>();
        }

        public void Register<TService>(TService value)
        {
            Register(typeof(TService), value);
        }

        public void Register(Type serviceType, object value)
        {
            if (_services.TryGetValue(serviceType, out var list))
                list.Add(value);
            else
                _services[serviceType] = new List<object> { value };
        }

        public object Get(Type serviceType)
        {
            return _services[serviceType].First();
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return _services[serviceType];
        }

        public TService Get<TService>()
        {
            return (TService)Get(typeof(TService));
        }

        public IEnumerable<TService> GetAll<TService>()
        {
            return GetAll(typeof(TService)).Cast<TService>();
        }
    }
}