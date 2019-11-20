using System;
using System.Text;

namespace HaLi.Tools.SecretMemory
{
    public static class Secret
    {
        public class Int : VAR<int>
        {
            internal Int(int v) : base(4)
            {
                Value = v;
            }

            protected override int BinaryToValue(byte[] bin)
                => BitConverter.ToInt32(bin, 0);

            protected override byte[] ValueToBinary(int value)
                => BitConverter.GetBytes(value);

            public static implicit operator Int(int v) => new Int(v);

            public static implicit operator int(Int v) => v.Value;
        }

        public class Long : VAR<long>
        {
            internal Long(long v) : base(8)
            {
                Value = v;
            }

            protected override long BinaryToValue(byte[] bin)
                => BitConverter.ToInt64(bin, 0);

            protected override byte[] ValueToBinary(long value)
                => BitConverter.GetBytes(value);

            public static implicit operator Long(long v) => new Long(v);

            public static implicit operator long(Long v) => v.Value;
        }

        public class Float : VAR<float>
        {
            internal Float(float v) : base(4)
            {
                Value = v;
            }

            protected override float BinaryToValue(byte[] bin)
                => BitConverter.ToSingle(bin, 0);

            protected override byte[] ValueToBinary(float value)
                => BitConverter.GetBytes(value);

            public static implicit operator Float(float v) => new Float(v);

            public static implicit operator float(Float v) => v.Value;
        }

        public class Double : VAR<double>
        {
            internal Double(double v) : base(8)
            {
                Value = v;
            }

            protected override double BinaryToValue(byte[] bin)
                => BitConverter.ToDouble(bin, 0);

            protected override byte[] ValueToBinary(double value)
                => BitConverter.GetBytes(value);

            public static implicit operator Double(double v) => new Double(v);

            public static implicit operator double(Double v) => v.Value;
        }

        public class Byte : VAR<byte>
        {
            internal Byte(byte v) : base(1)
            {
                Value = v;
            }

            protected override byte BinaryToValue(byte[] bin)
                => bin[0];

            protected override byte[] ValueToBinary(byte value)
                => new byte[1] { value };
        }

        public class Binary : VAR<byte[]>
        {
            internal Binary(byte[] v) : base(v.Length)
            {
                Value = v;
            }

            protected override byte[] BinaryToValue(byte[] bin)
                => bin;

            protected override byte[] ValueToBinary(byte[] value)
                => value;

            public static implicit operator Binary(byte[] v) => new Binary(v);

            public static implicit operator byte[](Binary v) => v.Value;
        }

        public class String : VAR<string>
        {
            internal String(string v) : base(Encoding.UTF8.GetByteCount(v))
            {
                Value = v;
                Length = v.Length;
            }

            public int Length { get; private set; }

            protected override string BinaryToValue(byte[] bin)
                => Encoding.UTF8.GetString(bin);

            protected override byte[] ValueToBinary(string value)
                => Encoding.UTF8.GetBytes(value);

            public static implicit operator String(string v) => new String(v);

            public static implicit operator string(String v) => v.Value;
        }
    }
}
