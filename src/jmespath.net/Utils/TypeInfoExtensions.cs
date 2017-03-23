using System;
using System.Reflection;

public static class TypeInfoExtensions
{
    public static FieldInfo GetField(this TypeInfo typeInfo, string name)
    {
        return typeInfo.BaseType.GetRuntimeField(name);
    }
}