using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HaLi.Tools.SecretMemory
{
    public class Block
    {
        private static readonly int[] PRIME = new int[] { 3, 7, 11, 13, 17, 23, 31, 37, 41, 47 };
        public BitArray used;
        public byte[] data;
        public int[] hash;

        public int Free { get; internal set; }

        private int next = 0;
        private int prime = 1;
        private object locker = new object();

        public Block() : this(1024) { }
        public Block(int size)
        {
            if (size <= 0)
                size = 1024;
            int r = 4 - (size % 4);
            if (r > 0 && r < 4)
                size += r;

            used = new BitArray(size, false);
            data = new byte[size];
            hash = new int[size / 4];
            Free = size;

            int tick = Environment.TickCount;
            prime = PRIME[tick % PRIME.Length];
            next = tick % size;

            var zero = S.CalcHash(new byte[32]);
            for (int i = 0; i < hash.Length; i++)
            {
                hash[i] = zero;
            }
        }

        internal bool Alloc(out int pos)
        {
            pos = 0;
            lock (locker)
            {
                if (Free > 0)
                {
                    while (used[next]) next = Move();
                    used[next] = true;
                    pos = next;
                    return true;
                }
            }
            return false;
        }

        internal void Release(int position)
        {
            used[position] = false;
            Free++;
            Console.WriteLine($"Relase:{position}");
        }

        internal bool Write(int p, byte value)
        {
            data[p] = value;

            // index  99 / 4 = 24
            int here = p / 4;
            int last = here - 1;
            if (last < 0) last = 255;

            Hash(p, out int low, out int high);
            hash[here] = low;
            hash[last] = high;

            Free--;
            return true;
        }

        internal byte Read(int p)
        {
            return data[p];
        }

        private int Move()
        {
            next += prime;
            next = Adjust(next);
            return next;
        }

        private int Adjust(int idx)
        {
            while (idx < 0) return idx + 1024;
            while (idx >= 1024) return idx - 1024;
            return idx;
        }

        public void Hash(int p, out int low, out int high)
        {
            // case 0
            // 0 1 2 3 4 5 6 7
            // 1020 1021 1022 1023 0 1 2 3

            // case 99
            // 96 97 98 99 100 101 102 103
            // 92 93 94 95 96 97 98 99
            p = p - (p % 4);

            low = S.CalcHash(
                new int[8]
                {
                    data[Adjust(p-4)],
                    data[Adjust(p-3)],
                    data[Adjust(p-2)],
                    data[Adjust(p-1)],
                    data[Adjust(p)],
                    data[Adjust(p+1)],
                    data[Adjust(p+2)],
                    data[Adjust(p+3)],
                });
            high = S.CalcHash(
                new int[8]
                {
                    data[Adjust(p)],
                    data[Adjust(p+1)],
                    data[Adjust(p+2)],
                    data[Adjust(p+3)],
                    data[Adjust(p+4)],
                    data[Adjust(p+5)],
                    data[Adjust(p+6)],
                    data[Adjust(p+7)],
                });
        }
    }
}
