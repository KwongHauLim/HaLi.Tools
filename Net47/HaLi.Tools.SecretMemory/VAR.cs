using System;

namespace HaLi.Tools.SecretMemory
{
    /// <summary>
    /// Basic class of secret data holder
    /// </summary>
    public abstract class VAR<T>
    {
        protected class Data
        {
            internal Block Block { get; set; }
            internal int Position { get; set; }

            public void Set(byte v) => Block.Write(Position, v);
            public byte Get() => Block.Read(Position);

            ~Data() => Block.Release(Position);
        }
        protected Data[] dats = null;
        protected int Size => dats?.Length ?? 0;

        protected Data this[int index] => dats[index];

        protected byte[] Bytes
        {
            get
            {
                var bin = new byte[Size];
                for (int i = 0; i < Size; i++)
                {
                    bin[i] = dats[i].Get();
                }
                return bin;
            }
            set
            {
                if (value != null && value.Length == Size)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        dats[i].Set(value[i]);
                    }
                }
            }
        }

        protected VAR(int size)
        {
            dats = new Data[size];
            for (int i = 0; i < size; i++)
            {
                dats[i] = Alloc();
            }
        }

        private Data Alloc()
        {
            Block block = Spy.GetFreeBlock();
            if (block.Alloc(out int pos))
            {
                return new Data
                {
                    Block = block,
                    Position = pos
                };
            }
            return Alloc(); // retry
        }

        public T Value
        {
            get => BinaryToValue(Bytes);
            set => Bytes = ValueToBinary(value);
        }

        protected abstract T BinaryToValue(byte[] bin);

        protected abstract byte[] ValueToBinary(T value);

        protected byte[] Binary(byte[] binary)
        {
            if (binary == null)
                return new byte[Size];
            if (binary.Length < Size)
                Array.Resize(ref binary, Size);
            return binary;
        }

        public override string ToString() => Value.ToString();
    }
}
