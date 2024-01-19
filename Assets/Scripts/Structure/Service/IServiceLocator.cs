using System;
using System.Collections.Generic;

namespace Structure.Service
{
    public interface IServiceLocator
    {
        void Register<TService>(TService value);
        void Register(Type serviceType, object value);
        object Get(Type serviceType);
        IEnumerable<object> GetAll(Type serviceType);
        TService Get<TService>();
        IEnumerable<TService> GetAll<TService>();
    }
}
