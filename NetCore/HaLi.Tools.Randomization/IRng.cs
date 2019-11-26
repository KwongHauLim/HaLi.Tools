namespace HaLi.Tools.Randomization
{
    /// <summary>
    /// (RNG)Random Number Generator
    /// </summary>
    public interface IRng
    {
        int Int32 { get; }
        uint UInt32 { get; }
        float Float { get; }
        double Double { get; }
        long Int64 { get; }
        ulong UInt64 { get; }
    }
}
