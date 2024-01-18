namespace HaLi.Tools.Randomization.Algorithms;

/// <summary>
/// xor-shift pseudo random number generator (RNG): xorshift128
/// <see cref="http://en.wikipedia.org/wiki/Xorshift"/> 
/// </summary>
public class XorShift : IRng
{
    private const double _BITSPACING_ = 1.0 / ((double)uint.MaxValue + 1.0);
    private UInt32 last;
    private UInt32 curr;

    public XorShift(uint seed)
    {
        last = seed;
        curr = 0;
        Next();
    }

    private uint Next()
    {
        UInt32 tmp = (last ^ (last << 11));
        last = curr;
        return curr = (curr ^ (curr >> 19)) ^ (tmp ^ (tmp >> 8));
    }

    public int Int32 => (int)UInt32;
    public uint UInt32 => Next();
    public float Float => (float)(Double % float.MaxValue);
    public double Double => _BITSPACING_ * UInt32;
    public long Int64 => (long)(UInt32 << 32 | UInt32);
    public ulong UInt64 => (ulong)(UInt32 << 32 | UInt32);
}

/// <summary>
/// xor-shift pseudo random number generator (RNG): xorwow
/// <see cref="http://en.wikipedia.org/wiki/Xorshift"/> 
public class XorWow : IRng
{
    private const double _BITSPACING_ = 1.0 / ((double)uint.MaxValue + 1.0);
    private UInt32 a, b, c, d;
    private UInt32 counter;

    public XorWow(uint seed)
    {
        a = seed;
        Next();
        Next();
        Next();
        Next();
    }

    private uint Next()
    {
        UInt32 t = d;
        UInt32 s = a;

        d = c;
        c = b;
        b = s;

        t ^= t >> 2;
        t ^= t << 1;
        t ^= s ^ (s << 4);
        a = t;

        unchecked { counter += 362437; }
        return t + counter;
    }

    public int Int32 => (int)UInt32;
    public uint UInt32 => Next();
    public float Float => (float)(Double % float.MaxValue);
    public double Double => _BITSPACING_ * UInt32;
    public long Int64 => (long)(UInt32 << 32 | UInt32);
    public ulong UInt64 => (ulong)(UInt32 << 32 | UInt32);
}
