using System.Diagnostics;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendDict
{
    public static V GetOrAdd<T, V>(this IDictionary<T, V> dict, T key, V value = default(V))
    {
        if (dict.ContainsKey(key))
            return dict[key];
        else
            return dict[key] = value;
    }

    public static V GetOrDefault<T, V>(this IDictionary<T, V> dict, T key, V @default = default(V))
    {
        if (dict.ContainsKey(key))
            return dict[key];
        else
            return @default;
    }

    public static V AddOrUpdate<T, V>(this IDictionary<T, V> dict, T key, dynamic inc)
    {
        if (dict.ContainsKey(key))
            dict[key] += inc;
        else
            dict[key] = inc;
        return dict[key];
    }

    public static void CopyTo<T, V>(this IDictionary<T, V> src, IDictionary<T, V> dst)
    {
        foreach (var pair in src)
        {
            dst[pair.Key] = pair.Value;
        }
    }
}
