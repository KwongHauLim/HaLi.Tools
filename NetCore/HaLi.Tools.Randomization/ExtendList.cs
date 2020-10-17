using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HaLi.Tools.Randomization
{
    [DebuggerStepThrough]
    public static class ExtendList
    {
        /// <summary>
        /// Shuffle the array
        /// </summary>
        public static void Shuffle<T>(this T[] array)
            => ShuffleAsync(array).Wait();

        /// <summary>
        /// Shuffle the array
        /// </summary>
        public static Task ShuffleAsync<T>(this T[] array)
            => Shuffle(array.Length, (a, b) =>
            {
                T tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            });

        /// <summary>
        /// Shuffle the List
        /// </summary>
        public static void Shuffle<T>(this List<T> list)
            => ShuffleAsync(list).Wait();

        /// <summary>
        /// Shuffle the List
        /// </summary>
        public static Task ShuffleAsync<T>(this List<T> list)
            => Shuffle(list.Count, (a, b) =>
            {
                T tmp = list[a];
                list[a] = list[b];
                list[b] = tmp;
            });

        private static Task Shuffle(int count, Action<int,int> indicate)
            => RNG.Share.Pool.DoAsync((rand) =>
            {
                // Fisher-Yates shuffle
                // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
                int idx;
                for (int i = count - 1; i > 0; i--)
                {
                    idx = Math.Abs(rand.Int32) % count;
                    if (idx != i)
                    {
                        indicate(idx, i);
                    }
                    Thread.Sleep(0);
                }
            });
    }
}
