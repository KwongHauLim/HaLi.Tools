using System;
using System.Collections.Generic;
using EasyPool;

namespace HaLi.Tools.SecretMemory
{
    /// <summary>
    /// Hold secret data, may be part, may be whole
    /// </summary>
    internal class Block
    {
        private readonly object locker = new object();

        /// <summary>
        /// Real data place
        /// </summary>
        internal byte[] data;
        /// <summary>
        /// Index for avail shot of data
        /// </summary>
        internal Queue<int> next;

        /// <summary>
        /// Byte Count of this block
        /// </summary>
        public int Size => data.Length;
        /// <summary>
        /// Number of empty shot in this block
        /// </summary>
        public int Free => next.Count;

        /// <summary>
        /// If found something, this block no more trustable
        /// </summary>
        internal bool Trust { get; set; }

        internal class EventArgs
        {
            public int Position { get; set; }
            public byte Value { get; set; }
        }
        internal event EventHandler<EventArgs> BeforeWrite;
        internal event EventHandler<EventArgs> AfterWrite;
        internal event EventHandler<EventArgs> BeforeRead;
        internal event EventHandler<EventArgs> AfterRead;

        private Pool<EventArgs> pool;

        public Block() : this(1024) { }
        public Block(int size)
        {
            if (size <= 0)
                size = 1024;

            data = new byte[size];
            next = new Queue<int>(Prime.GetShuffle(size));

            pool = new Pool<EventArgs>();

            Trust = true;
        }

        internal bool Alloc(out int pos)
        {
            pos = 0;
            lock (locker)
            {
                if (Free > 0)
                {
                    pos = next.Dequeue();
                    return true;
                }
            }
            return false;
        }

        internal void Release(int position)
        {
            next.Enqueue(position);
        }

        internal void Write(int p, byte value)
        {
            var args = pool.Pop();
            args.Position = p;
            args.Value = data[p];

            BeforeWrite?.Invoke(this, args);
            if (!Trust)
                Spy.Protect.HealMe(this);
            data[p] = value;
            args.Value = value;
            AfterWrite?.Invoke(this, args);

            pool.Push(args);
        }

        internal byte Read(int p)
        {
            var args = pool.Pop();
            args.Position = p;
            args.Value = data[p];

            BeforeRead?.Invoke(this, args);
            if (!Trust)
                Spy.Protect.HealMe(this);
            byte d = data[p];
            AfterRead?.Invoke(this, args);

            pool.Push(args);

            return d;
        }
    }
}
