using System.Collections;

namespace HaLi.Tools.EasyLock;

public class LockDict<T1, T2> : IDictionary<T1, T2>
{
    private readonly object Locker = new object();
    public Dictionary<T1, T2> Origin { get; private set; } = new Dictionary<T1, T2>();

    public T2 this[T1 key]
    {
        get { lock (Locker) { return Origin[key]; } }
        set { lock (Locker) { Origin[key] = value; } }
    }

    public ICollection<T1> Keys { get { lock (Locker) { return Origin.Keys; } } }
    public ICollection<T2> Values { get { lock (Locker) { return Origin.Values; } } }
    public int Count { get { lock (Locker) { return Origin.Count; } } }
    public bool IsReadOnly => false;

    public void Add(T1 key, T2 value)
    {
        lock (Locker)
        {
            Origin.Add(key, value);
        }
    }

    public void Add(KeyValuePair<T1, T2> item) => Add(item.Key, item.Value);

    public void Clear()
    {
        lock (Locker)
        {
            Origin.Clear();
        }
    }

    public bool Contains(KeyValuePair<T1, T2> item)
    {
        lock (Locker) { return Origin.TryGetValue(item.Key, out T2 v) && v.Equals(item.Value); }
    }

    public bool ContainsKey(T1 key)
    {
        lock (Locker) { return Origin.ContainsKey(key); }
    }

    public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
    {
        lock (Locker)
        {
            if (array == null)
                array = new KeyValuePair<T1, T2>[Origin.Count + arrayIndex];
            else
                Array.Resize(ref array, Origin.Count + arrayIndex);

            int i = arrayIndex;
            foreach (var pair in Origin)
            {
                array[i] = pair;
                i++;
            }
        }
    }

    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
    {
        lock (Locker) { return Origin.GetEnumerator(); }
    }

    public bool Remove(T1 key)
    {
        lock (Locker) { return Origin.Remove(key); }
    }

    public bool Remove(KeyValuePair<T1, T2> item)
    {
        lock (Locker)
        {
            if (Origin.TryGetValue(item.Key, out T2 v) && v.Equals(item.Value))
                Origin.Remove(item.Key);
        }
        return true;
    }

    public bool TryGetValue(T1 key, out T2 value)
    {
        lock (Locker) { return Origin.TryGetValue(key, out value); }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        lock (Locker) { return Origin.GetEnumerator(); }
    }
}
