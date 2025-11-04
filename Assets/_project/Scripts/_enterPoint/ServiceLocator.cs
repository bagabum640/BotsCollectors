using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services = new();

    public static void Register<T>(object service)
    {
        Services[typeof(T)] = service;
    }

    public static object GetService(Type argumentType)
    {
        if (Services.TryGetValue(argumentType, out var service))
            return service;

        throw new Exception($"Service {argumentType.Name} not registred");
    }   
}