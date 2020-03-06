using System;

namespace HaLi.Tools.Extends
{
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
    } 
}
