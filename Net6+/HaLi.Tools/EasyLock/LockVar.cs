namespace HaLi.Tools.EasyLock;

public abstract class LockVar<T> : IEquatable<T>, IComparable<T>
{
    protected readonly object _Locker = new object();
    protected T _Var = default(T);
    public T Value
    {
        get
        {
            T tmp;
            lock (_Locker) { tmp = _Var; }
            return tmp;
        }
        set
        {
            lock (_Locker)
            {
                _Var = value;
            }
        }
    }


    public abstract int CompareTo(T other);
    public abstract bool Equals(T other);
}
