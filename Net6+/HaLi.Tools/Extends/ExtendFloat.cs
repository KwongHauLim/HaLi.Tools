using System.Diagnostics;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendFloat
{
    public static float Round(this float f, int decimals)
    {
        decimal dec = new decimal(f);
        return Convert.ToSingle(decimal.Round(dec, decimals));
    }

    public static float Ceil(this float f)
    {
        return (float)Math.Ceiling(f);
    }

    public static float ModOne(this float f)
    {
        return f - (int)f;
    }

    public static bool AboveZero(this float f)
    {
        return f.CompareTo(0f) > 0;
    }

    public static bool UnderZero(this float f)
    {
        return f.CompareTo(0f) < 0;
    }

    public static bool EqualZero(this float f)
    {
        return f.CompareTo(0f) == 0;
    }

    public static string ToBase64(this float f)
    {
        return Convert.ToBase64String(BitConverter.GetBytes(f));
    }

    public static bool InRange(this float f, float min, float max)
        => f.CompareTo(min) >= 0 && f.CompareTo(max) <= 0;

    public static bool InRange01(this float f)
        => f.CompareTo(0f) >= 0 && f.CompareTo(1f) <= 0;

    public static float Clamp(this float f, float min, float max)
    {
        if (f.CompareTo(min) < 0)
            return min;
        if (f.CompareTo(max) > 0)
            return max;
        return f;
    }

    public static float Clamp01(this float f)
    {
        if (f.CompareTo(0f) < 0)
            return 0f;
        if (f.CompareTo(1f) > 0)
            return 1f;
        return f;
    }
}
