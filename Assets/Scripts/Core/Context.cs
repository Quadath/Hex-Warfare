namespace Core
{
    using System;  
    using System.Collections.Generic;  
  
    public class Context  
    {  
        private readonly Dictionary<Type, object> services = new();  
  
        public void Register<T>(T instance) where T : class {  
            services[typeof(T)] = instance;  
        }  
        public T Resolve<T>() where T : class  
        {  
            if (services.TryGetValue(typeof(T), out var instance))  
                return (T)instance;  
  
            throw new InvalidOperationException($"Service {typeof(T)} is not registered");  
        }  
    }
}