using System.Collections.Generic;
using System;

public static class ServiceLocator
{
    private static Dictionary<Type, object> servies = new Dictionary<Type, object>();

    public static void Register<T>(T service) => servies[typeof(T)] = service;

    public static T Get<T>() => servies.TryGetValue(typeof(T), out var service) ? (T)service : default;
}