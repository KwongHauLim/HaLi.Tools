using System.Diagnostics;
using System.Reflection;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendObject
{
    public static void CloneTo<T1, T2>(T1 src, T2 dst)
        where T1 : class
        where T2 : T1
    {
        if (src != null && dst != null)
        {
            BindingFlags copy = (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            FieldInfo[] fields = typeof(T1).GetFields(copy);
            foreach (var item in fields)
            {
                item.SetValue(dst, item.GetValue(src));
            }
        }
    }

    public static T2 Duplicate<T1, T2>(T1 parent)
        where T1 : class
        where T2 : T1, new()
    {
        T2 cls = new T2();
        CloneTo(parent, cls);
        return cls;
    }
}
