using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.SecretMemory
{
    internal class Prime
    {
        private static readonly int[] PRIME = new int[] { 3, 7, 11, 13, 17, 23, 31, 37, 41, 47 };

        public static int Get()
            => PRIME[Environment.TickCount % PRIME.Length];

        public static int[] GetShuffle(int size)
        {
            int[] list = new int[size];
            int prime = Get();
            int next = Environment.TickCount % size;

            for (int i = 0; i < size; i++)
            {
                list[i] = next;
                next = (next + prime) % size;
            }

            return list;
        }
    }
}
