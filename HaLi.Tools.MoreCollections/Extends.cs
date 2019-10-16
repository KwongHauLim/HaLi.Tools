using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.MoreCollections
{
    public static class Extends
    {
        public static void Shuffle<T>(this List<T> list)
        {
            Random rand = new Random();

            // Fisher-Yates shuffle
            // http://en.wikipedia.org/wiki/Fisher-Yates_shuffle

            T tmp;
            int idx = 0;
            for (int i = list.Count - 1; i >= 2; i--)
            {
                idx = rand.Next(i);
                tmp = list[idx];
                list[idx] = list[i];
                list[i] = tmp;
            }
        }
    }
}
