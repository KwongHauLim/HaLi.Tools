using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HaLi.Tools.EasyLock
{
    public class LockList<T> : IList<T>
    {
        private readonly object Locker = new object();
        public List<T> Origin { get; private set; } = new List<T>();

        public T this[int index]
        {
            get { lock (Locker) { return Origin[index]; } }
            set { lock (Locker) { Origin[index] = value; } }
        }

        public int Count { get { lock (Locker) { return Origin.Count; } } }
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            lock (Locker) { Origin.Add(item); }
        }

        public void Clear()
        {
            lock (Locker) { Origin.Clear(); }
        }

        public bool Contains(T item)
        {
            lock (Locker) { return Origin.Contains(item); }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (Locker)
            {
                if (array == null)
                    array = new T[Origin.Count + arrayIndex];
                else
                    Array.Resize(ref array, Origin.Count + arrayIndex);

                Origin.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (Locker) { return Origin.GetEnumerator(); }
        }

        public int IndexOf(T item)
        {
            lock (Locker) { return Origin.IndexOf(item); }
        }

        public void Insert(int index, T item)
        {
            lock (Locker) { Origin.Insert(index, item); }
        }

        public bool Remove(T item)
        {
            lock (Locker) { return Origin.Remove(item); }
        }

        public void RemoveAt(int index)
        {
            lock (Locker) { Origin.RemoveAt(index); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (Locker) { return Origin.GetEnumerator(); }
        }
    }
}
