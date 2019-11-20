using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaLi.Tools.EasyLock
{
    public class AB<T>
        where T: class, new()
    {
        public class Locker
        {
            private readonly object locker = new object();
            public T data = new T();
            public bool Avail { get; private set; }

            public void Hold(Action<T> action)
            {
                lock (locker)
                {
                    Avail = false;
                    try
                    {
                        action(data);
                    }
                    finally
                    {
                        Avail = true;
                    }
                }
            }
        }

        private List<Locker> list = new List<Locker>();
        public bool CouldExpand { get; set; }
        private int balance = 0;

        public AB() : this(4, false) { }
        public AB(int num, bool expand)
        {
            CouldExpand = expand;
            for (int i = 0; i < num; i++)
            {
                list.Add(new Locker());
            }
        }

        public void Foreach(Action<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Hold(action);
            }
        }

        public void Lock(Action<T> action)
        {
            balance %= list.Count;
            var avail = Avail();
            balance++;
            avail.Hold(action);
        }

        private Locker Avail()
        {
            int a = balance;
            int b = list.Count;

            for (int i = a; i < b; i++)
            {
                if (list[i].Avail)
                    return list[i];
            }

            for (int i = 0; i < a; i++)
            {
                if (list[i].Avail)
                    return list[i];
            }

            if (CouldExpand || list.Count <= 0)
                list.Add(new Locker());

            return list[list.Count - 1];
        }
    }
}
