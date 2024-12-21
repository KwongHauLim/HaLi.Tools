namespace HaLi.Tools.Maths;

public static class Bits
{
    public static byte ReverseBits(byte b) { ReverseBits(ref b); return b; }
    public static int ReverseBits(int n) => (int)ReverseBits((uint)n);
    public static long ReverseBits(long n) => (long)ReverseBits((ulong)n);
    public static ulong ReverseBits(ulong n) { ReverseBits(ref n); return n; }
    public static uint ReverseBits(uint n) { ReverseBits(ref n); return n; }

    public static void ReverseBits(ref uint n)
    {
        n = (n >> 1) & 0x55555555 | (n << 1) & 0xaaaaaaaa;
        n = (n >> 2) & 0x33333333 | (n << 2) & 0xcccccccc;
        n = (n >> 4) & 0x0f0f0f0f | (n << 4) & 0xf0f0f0f0;
        n = (n >> 8) & 0x00ff00ff | (n << 8) & 0xff00ff00;
        n = (n >> 16) & 0x0000ffff | (n << 16) & 0xffff0000;
    }
    public static void ReverseBits(ref ulong n)
    {
        n = (n >> 1) & 0x5555555555555555 | (n << 1) & 0xaaaaaaaaaaaaaaaa;
        n = (n >> 2) & 0x3333333333333333 | (n << 2) & 0xcccccccccccccccc;
        n = (n >> 4) & 0x0f0f0f0f0f0f0f0f | (n << 4) & 0xf0f0f0f0f0f0f0f0;
        n = (n >> 8) & 0x00ff00ff00ff00ff | (n << 8) & 0xff00ff00ff00ff00;
        n = (n >> 16) & 0x0000ffff0000ffff | (n << 16) & 0xffff0000ffff0000;
        n = (n >> 32) & 0x00000000ffffffff | (n << 32) & 0xffffffff00000000;
    }

    public static void ReverseBits(ref byte b)
    {
        b = (byte)((b * 0x0202020202 & 0x010884422010) % 1023);
    }
}
