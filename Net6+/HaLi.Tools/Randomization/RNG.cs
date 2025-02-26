﻿using HaLi.Tools.EasyLock;
using HaLi.Tools.Randomization.Algorithms;

namespace HaLi.Tools.Randomization;

public sealed class RNG
{
    private static RNG _ptr = null;
    public static RNG Share => _ptr = _ptr ?? new RNG();

    internal LockReplier<IRng> Pool { get; set; } = new LockReplier<IRng>();
    internal uint Seed { get; set; } = 0;

    private RNG() : this((uint)Environment.TickCount) { }

    private RNG(uint seed)
    {
        Seed = seed;
        var first = new XorWow(seed);
        Pool.Add(first);

        for (int i = 1; i < 4; i++)
        {
            Pool.Add(new XorWow(first.UInt32));
        }
    }

    public void Clear()
    {
        Pool.Clear();
    }

    public void Add<T>(T item)
        where T : IRng
    {
        if (item == null)
            throw new ArgumentNullException();

        Pool.Add(item);
    }

    private static T Do<T>(Func<IRng, T> func)
    {
        T val = default;
        Share.Pool.DoAsync((rand) =>
        {
            val = func(rand);
        }).Wait();
        return val;
    }

    public static int Int32 => Do(rng => rng.Int32);

    public static long Int64 => Do(rng => rng.Int64);

    public static uint UInt32 => Do(rng => rng.UInt32);

    public static ulong UInt64 => Do(rng => rng.UInt64);

    public static float Float => Do(rng => rng.Float);

    public static double Double => Do(rng => rng.Double);

    public static int Next(int min = int.MinValue, int max = int.MaxValue)
    {
        int r = 0;
        Share.Pool.DoAsync((rand) =>
        {
            r = (int)Math.Round(rand.Double * (max - min) + min);
        }).Wait();
        return r;
    }

    public static double Next(double min = double.MinValue, double max = double.MaxValue)
    {
        double r = 0.0;
        Share.Pool.DoAsync((rand) =>
        {
            r = (double)UInt32 / (double)uint.MaxValue * (max - min) + min;
        }).Wait();
        return r;
    }

    public static void Fill<T>(T[] array)
    {
        if (array == null) throw new ArgumentNullException();
        if (typeof(T).IsValueType)
        {
            var code = Type.GetTypeCode(typeof(T));
            Share.Pool.DoAsync(rng =>
            {
                if (code == TypeCode.Int32)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)rng.Int32; }
                else if (code == TypeCode.Int64)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)rng.Int64; }
                else if (code == TypeCode.UInt32)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)rng.UInt32; }
                else if (code == TypeCode.UInt64)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)rng.UInt64; }
                else if (code == TypeCode.Single)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)rng.Float; }
                else if (code == TypeCode.Double)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)rng.Double; }
                else if (code == TypeCode.Byte)
                    for (int i = 0; i < array.Length; i++) { array[i] = (T)(object)(byte)(rng.Int32 & 0xFF); }
            }).Wait();
        }
    }

    public static T Random<T>(T[] array)
    {
        if (array == null) throw new ArgumentNullException();
        if (array.Length == 1) return array[0];
        return array[UInt32 % array.Length];
    }

    public static T Random<T>(List<T> list)
    {
        if (list == null) throw new ArgumentNullException();
        if (list.Count == 1) return list[0];
        return list[Math.Abs(Int32) % list.Count];
    }
}