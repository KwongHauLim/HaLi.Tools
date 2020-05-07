using System;
using System.Collections.Generic;

namespace HaLi.Tools.EasyLock
{
    public class LockStack<T> where T : class
    {
        protected readonly object _Locker = new object();
        protected Stack<T> stack = new Stack<T>();

        public int Avail { get { return stack.Count; } }

        public virtual void Push(T cls)
        {
            lock (_Locker)
            {
                stack.Push(cls);
            }
        }

        public virtual T Pop()
        {
            T cls = null;
            lock (_Locker)
            {
                if (stack.Count > 0)
                    cls = stack.Pop();
            }
            return cls;
        }
    }

    public class LockPool<T> : LockStack<T> where T : class, new()
    {
        public int Limit { get; set; } = 32;

        public LockPool() : this(0) { }
        public LockPool(int perpare)
        {
            for (int i = 0; i < perpare; i++)
            {
                Push(New());
            }
        }

        public virtual T New() => new T();

        public override void Push(T cls)
        {
            lock (_Locker)
            {
                if (stack.Count < Limit)
                    stack.Push(cls);
            }
        }

        public override T Pop()
        {
            return base.Pop() ?? PopEmpty();
        }

        protected virtual T PopEmpty()
        {
            return New();
        }
    }
}
