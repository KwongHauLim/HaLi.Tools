using System.Diagnostics;

namespace HaLi.Tools.Extends;

[DebuggerStepThrough]
public static class ExtendList
{
    public static IEnumerable<int> Range(int count, int start = 0, bool full = false)
    {
        for (int i = 0; i < count; i++)
        {
            yield return start + i;
        }

        for (int i = 0; i < start && full; i++)
        {
            yield return i;
        }
    }

    public static bool Check<T>(this List<T> list, int index)
        => index >= 0 && index < list.Count;

    public static T At<T>(this List<T> list, int index, T @default = default)
        => Check(list, index) ? list[index] : @default;

    public static T First<T>(this List<T> list, T @default = default)
        => At(list, 0, @default);

    public static T Last<T>(this List<T> list, T @default = default)
        => At(list, list.Count - 1, @default);

    public static T First<T>(this List<T> list, Func<T, bool> predicate, T @default = default)
    {
        foreach (var item in list)
        {
            if (predicate(item))
                return item;
        }
        return @default;
    }

    public static int Near<T>(this List<T> list, T find)
        where T : IComparable
    {
        int mid;
        int lo = 0;
        int hi = list.Count - 1;
        while (hi - lo > 1)
        {
            mid = (lo + hi) / 2;
            if (list[mid].CompareTo(find) < 0)
                lo = mid;
            else
                hi = mid;
        }

        return lo;
    }

    public static T Random<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            Random rand = new Random();
            int index = rand.Next(list.Count);
            return list[index];
        }
        return default;
    }

    public static IEnumerable<T> Random<T>(this List<T> list, int count, bool unique = true)
    {
        Random rand = new Random();
        List<int> map = new List<int>(Range(0, list.Count));
        for (int i = 0; i < count && map.Count > 0; i++)
        {
            int idx = rand.Next(map.Count);
            yield return list[map[idx]];

            if (unique)
                map.RemoveAt(idx);
        }
    }

    public static IEnumerable<U> Cast<T, U>(this List<T> list, Func<T, U> cast)
    {
        foreach (var item in list)
        {
            yield return cast(item);
        }
    }

    public static IEnumerable<T> Condition<T>(this List<T> list, Func<T, bool> condition)
    {
        foreach (var item in list)
        {
            if (condition(item))
                yield return item;
        }
    }

    public static IEnumerable<T> ExceptNull<T>(this List<T> list) where T : class
        => Condition(list, (item) => item != null);

    public static void AddUnqiue<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }

    public static void AddUnqiue<T>(this List<T> list, IEnumerable<T> others)
    {
        foreach (var item in others)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }
    }

    public static void AddRepeat<T>(this List<T> list, T obj, int count)
    {
        if (count > 0)
        {
            T[] tmp = new T[count];
            for (int i = 0; i < count; i++)
                tmp[i] = obj;
            list.AddRange(tmp);
        }
    }

    public static T Pop<T>(this List<T> list)
    {
        if (list.Count>0)
        {
            int index = list.Count - 1;
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
        return default;
    }

    public static List<T> Migrate<T>(this List<T> list, List<T> other)
    {
        var tmp = new List<T>();
        tmp.AddRange(list);
        tmp.AddRange(other);
        return tmp;
    }

    public static List<T> Migrate<T>(this List<T> list, List<T> other, Func<T, bool> condition)
    {
        var tmp = new List<T>();
        tmp.AddRange(Condition(list, condition));
        tmp.AddRange(Condition(other, condition));
        return tmp;
    }

    public static string ToString<T>(this List<T> list, Func<T, string> fn, string separator = ",")
        => string.Join(separator, Cast<T, string>(list, fn));
}
