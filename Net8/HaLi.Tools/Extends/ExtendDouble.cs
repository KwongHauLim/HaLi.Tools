using System.Diagnostics;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendDouble
{
    public static double Round(this double f, int decimals)
    {
        decimal dec = new decimal(f);
        return Convert.ToDouble(decimal.Round(dec, decimals));
    }

    public static double Ceil(this double f)
    {
        return Math.Ceiling(f);
    }

    public static double ModOne(this double f)
    {
        return f - (int)f;
    }

    public static bool AboveZero(this double f)
    {
        return f.CompareTo(0.0) > 0;
    }

    public static bool UnderZero(this double f)
    {
        return f.CompareTo(0.0) < 0;
    }

    public static bool EqualZero(this double f)
    {
        return f.CompareTo(0.0) == 0;
    }

    public static string ToBase64(this double f)
    {
        return Convert.ToBase64String(BitConverter.GetBytes(f));
    }

    public static bool InRange(this double f, double min, double max)
        => f.CompareTo(min) >= 0 && f.CompareTo(max) <= 0;

    public static bool InRange01(this double f)
        => f.CompareTo(0f) >= 0 && f.CompareTo(1f) <= 0;

    public static double Clamp(this double f, double min, double max)
    {
        if (f.CompareTo(min) < 0)
            return min;
        if (f.CompareTo(max) > 0)
            return max;
        return f;
    }

    public static double Clamp01(this double f)
    {
        if (f.CompareTo(0f) < 0)
            return 0f;
        if (f.CompareTo(1f) > 0)
            return 1f;
        return f;
    }
}
