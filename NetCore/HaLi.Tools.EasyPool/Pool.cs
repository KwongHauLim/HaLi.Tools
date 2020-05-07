using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyPool
{
    public class Pool<T>
        where T : class, new()
    {
        private readonly object locker = new object();

        private Stack<T> stack = new Stack<T>();

        public bool NewIfEmpty { get; set; } = true;

        public void Push(T v)
        {
            lock (locker)
            {
                stack.Push(v); 
            }
        }

        public T Pop()
        {
            lock (locker)
            {
                if (stack.Count > 0)
                    return stack.Pop();
            }

            return PopEmpty();
        }

        protected virtual T PopEmpty()
        {
            if (NewIfEmpty)
                return new T();
            else
                return default(T);
        }

    }
}
