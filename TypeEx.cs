using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ConnectorLib.JSON;

public static class TypeEx
{
    public static bool IsAssignableTo(this Type? type, Type? targetType)
    {
#if NETSTANDARD1_0 || NETSTANDARD1_3
        return type is not null && targetType is not null && targetType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
#else
        return type is not null && targetType is not null && targetType.IsAssignableFrom(type);
#endif
    }
}