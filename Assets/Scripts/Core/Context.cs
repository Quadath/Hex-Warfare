namespace Core
{
    using System;  
    using System.Collections.Generic;  
  
    //Simple container which provide "context" to constructors 
    public class Context  
    {  
        private readonly Dictionary<Type, object> _services = new();  
  
        public void Register<T>(T instance) where T : class {  
            _services[typeof(T)] = instance;  
        }  
        public T Resolve<T>() where T : class  
        {  
            if (_services.TryGetValue(typeof(T), out var instance))  
                return (T)instance;  
  
            throw new InvalidOperationException($"Service {typeof(T)} is not registered");  
        }  
    }
}