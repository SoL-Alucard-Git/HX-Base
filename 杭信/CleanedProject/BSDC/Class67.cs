using System;
using System.Reflection;
using System.Runtime.CompilerServices;

internal class Class67
{
    internal static Module module_0;

    static Class67()
    {
        Class71.smethod_0();
        module_0 = typeof(Class67).Assembly.ManifestModule;
    }

    public Class67()
    {
        Class71.smethod_0();
    }

    internal static void smethod_0(int typemdt)
    {
        Type type = module_0.ResolveType(0x2000000 + typemdt);
        foreach (FieldInfo info in type.GetFields())
        {
            MethodInfo method = (MethodInfo) module_0.ResolveMethod(info.MetadataToken + 0x6000000);
            info.SetValue(null, (MulticastDelegate) Delegate.CreateDelegate(type, method));
        }
    }

    internal delegate void Delegate25(object o);
}

