using EasyLock;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FlowService.Tools
{
    // xor-shift pseudo random number generator (RNG)
    // http://en.wikipedia.org/wiki/Xorshift
    public class RNG
    {
        private static RNG _ptr = null;
        public static RNG Share { get { return _ptr ?? (_ptr = new RNG()); } }
      
        private const double _BITSPACING_ = 1.0 / ((double)uint.MaxValue + 1.0);

        private class Pseudo
        {
            private UInt32 last;
            private UInt32 curr;

            public Pseudo(uint seed)
            {
                last = seed;
                curr = 0;
                Next();
            }

            public uint Next()
            {
                UInt32 tmp = (last ^ (last << 11));
                last = curr;
                return curr = (curr ^ (curr >> 19)) ^ (tmp ^ (tmp >> 8));
            }
        }
        private LockStack<Pseudo> stack = new LockStack<Pseudo>();
        private uint seed = 0;

        public RNG()
        {
            seed = (uint)Environment.TickCount;
            CreatePseudo(32, seed);
        }

        public RNG(uint seed)
        {
            this.seed = seed;
            CreatePseudo(32, seed);
        }

        private void CreatePseudo(int num, uint seed = 0)
        {
            byte[] xor = new byte[num * 4];
            RNGCryptoServiceProvider.Create().GetNonZeroBytes(xor);

            for (int i = 0; i < num; i++)
            {
                uint se = BitConverter.ToUInt32(xor, i * 4) ^ seed;
                stack.Push(new Pseudo(se));
            }
        }

        private uint Next()
        {
            if (stack.Avail <= 0)
                CreatePseudo(4, seed);

            Pseudo pseudo = stack.Pop();
            uint ret = pseudo.Next();
            stack.Push(pseudo);

            return ret;
        }

        public int Int32 => (int)UInt32;
        public uint UInt32 => Next();
        public float Float => (float)(Double % float.MaxValue);
        public double Double => _BITSPACING_ * UInt32;
        public long Int64 => (long)(UInt32 << 32 | UInt32);
        public ulong UInt64 => (ulong)(UInt32 << 32 | UInt32);

        public void Shuffle<T>(List<T> list)
        {
            if (list == null)
                return;

            for (int i = list.Count - 1; i >= 1; i--)
            {
                int r = Math.Abs(Int32) % i;
                var t = list[r];
                list[r] = list[i];
                list[i] = t;
            }
        }
    }

}