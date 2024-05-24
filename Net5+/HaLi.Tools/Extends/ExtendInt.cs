using System.Diagnostics;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendInt
{
    public static decimal PercentBy(this int i, int div)
    {
        return PercentBy((uint)i, (uint)div);
    }
    public static decimal PercentBy(this uint i, uint div)
    {
        if (i == 0 || div == 0)
            return 0M;

        decimal dec = i * 100M;
        dec = decimal.Round(decimal.Divide(dec, div), 2);
        return dec;
    }

    public static string ToBase64(this int f) => Convert.ToBase64String(BitConverter.GetBytes(f));

    public static string ToBase64(this uint f) => Convert.ToBase64String(BitConverter.GetBytes(f));

    public static int Add(this int i, dynamic v)
    {
        try { checked { i += (int)v; } }
        catch { i = (v > 0) ? int.MaxValue : int.MinValue; }
        return i;
    }
    public static uint Add(this uint i, dynamic v)
    {
        try { checked { i += (uint)v; } }
        catch { i = (v > 0) ? uint.MaxValue : uint.MinValue; }
        return i;
    }
    public static long Add(this long i, dynamic v)
    {
        try { checked { i += (long)v; } }
        catch { i = (v > 0) ? long.MaxValue : long.MinValue; }
        return i;
    }
    public static ulong Add(this ulong i, dynamic v)
    {
        try { checked { i += (ulong)v; } }
        catch { i = (v > 0) ? ulong.MaxValue : ulong.MinValue; }
        return i;
    }

    public static int Sub(this int i, dynamic v)
    {
        try { checked { i -= (int)v; ; } }
        catch { i = (v > 0) ? int.MinValue : int.MaxValue; }
        return i;
    }
    public static uint Sub(this uint i, dynamic v)
    {
        try { checked { i -= (uint)v; ; } }
        catch { i = (v > 0) ? uint.MinValue : uint.MaxValue; }
        return i;
    }
    public static long Sub(this long i, dynamic v)
    {
        try { checked { i -= (long)v; ; } }
        catch { i = (v > 0) ? long.MinValue : long.MaxValue; }
        return i;
    }
    public static ulong Sub(this ulong i, dynamic v)
    {
        try { checked { i -= (ulong)v; ; } }
        catch { i = (v > 0) ? ulong.MinValue : ulong.MaxValue; }
        return i;
    }

    public static int Inc(this int i) { return i.Add(1); }
    public static uint Inc(this uint i) { return i.Add(1); }
    public static long Inc(this long i) { return i.Add(1); }
    public static ulong Inc(this ulong i) { return i.Add(1); }

    public static int Dec(this int i) { return i.Sub(1); }
    public static uint Dec(this uint i) { return i.Sub(1); }
    public static long Dec(this long i) { return i.Sub(1); }
    public static ulong Dec(this ulong i) { return i.Sub(1); }
}
