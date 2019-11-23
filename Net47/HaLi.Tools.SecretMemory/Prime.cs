using HaLi.Tools.Randomization;

namespace HaLi.Tools.SecretMemory
{
    internal class Prime
    {
        private static readonly int[] PRIME = new int[] { 3, 7, 11, 13, 17, 23, 31, 37, 41, 47 };

        public static int Get()
            => PRIME[RNG.Next(0, PRIME.Length)];

        public static int[] GetShuffle(int size)
        {
            int[] list = new int[size];
            int prime = Get();
            int next = RNG.Next(0, size);

            for (int i = 0; i < size; i++)
            {
                list[i] = next;
                next = (next + prime) % size;
            }

            return list;
        }
    }
}
