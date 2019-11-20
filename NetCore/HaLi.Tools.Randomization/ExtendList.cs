using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HaLi.Tools.Randomization
{
    [DebuggerStepThrough]
    public static class ExtendList
    {
        public static async void Shuffle<T>(this List<T> list)
            => await ShuffleAsync(list);

        public static Task ShuffleAsync<T>(this List<T> list)
            => RNG.Share.Pool.DoAsync((rand) =>
            {
                // Fisher-Yates shuffle
                // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
                T tmp;
                int cnt = list.Count;
                int idx;
                for (int i = cnt - 1; i > 0; i--)
                {
                    idx = rand.Int32 % cnt;
                    if (idx != i)
                    {
                        tmp = list[idx];
                        list[idx] = list[i];
                        list[i] = tmp;
                    }
                    Thread.Sleep(0);
                }
            });

    }
}
