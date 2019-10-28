using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using HaLi.Tools.EasyLock;
using HaLi.Tools.Randomization.Algorithms;

namespace HaLi.Tools.Randomization
{
    // xor-shift pseudo random number generator (RNG)
    // http://en.wikipedia.org/wiki/Xorshift
    public class RNG
    {
        private static RNG _ptr = null;
        public static RNG Share => _ptr = _ptr ?? new RNG();

        internal protected LockReplier<IRng> Pool { get; set; } = new LockReplier<IRng>();
        internal protected uint Seed { get; set; }  = 0;

        public RNG() : this((uint)Environment.TickCount) { }
        public IRng Algorithm { get; set; }

        public RNG(uint seed)
        {
            Seed = seed;
            var first = new XorShift(seed);
            Pool.Add(first);

            for (int i = 1; i < 4; i++)
            {
                Pool.Add(new XorShift(first.UInt32));
            }
        }

        public void Push<T>(T item)
            where T : IRng
        {
            if (item == null)
                throw new ArgumentNullException();

            Pool.Add(item);
        }
    }

}