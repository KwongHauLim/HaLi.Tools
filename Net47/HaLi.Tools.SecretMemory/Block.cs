using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HaLi.Tools.Randomization;

namespace HaLi.Tools.SecretMemory
{
    public class Block
    {
        public BitArray used;
        public byte[] data;

        public int Free { get; internal set; }

        private int next = 0;
        private int prime = 1;
        private object locker = new object();

        internal event EventHandler BeforeWrite;
        internal event EventHandler AfterWrite;
        internal event EventHandler BeforeRead;
        internal event EventHandler AfterRead;

        public Block() : this(1024) { }
        public Block(int size)
        {
            if (size <= 0)
                size = 1024;

            used = new BitArray(size, false);
            data = new byte[size];
            Free = size;

            prime = Prime.Get();
            next = RNG.Next(0, size);
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
                    Free--;
                    return true;
                }
            }
            return false;
        }

        internal void Release(int position)
        {
            used[position] = false;
            Free++;
        }

        internal void Write(int p, byte value)
        {
            if (BeforeWrite != null)
                BeforeWrite(this, EventArgs.Empty);

            data[p] = value;

            if (AfterWrite != null)
                AfterWrite(this, EventArgs.Empty);
        }

        internal byte Read(int p)
        {
            if (BeforeRead != null)
                BeforeRead(this, EventArgs.Empty);

            byte d = data[p];

            if (AfterRead != null)
                AfterRead(this, EventArgs.Empty);

            return d;
        }

        private int Move()
        {
            next += prime;
            next = Adjust(next);
            return next;
        }

        private int Adjust(int idx)
        {
            int size = data.Length;
            while (idx < 0) return idx + size;
            while (idx >= size) return idx - size;
            return idx;
        }
    }
}
