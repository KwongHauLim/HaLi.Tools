namespace HaLi.Tools.EasyLock;

public class LockReplier<T> where T : class
{
    protected class Replier
    {
        public int index;
        public T Item;
        public bool isAvail;
    }
    protected readonly object locker = new object();
    protected List<Replier> list = new List<Replier>();
    protected Queue<int> avails = new Queue<int>();

    public int Avail => avails.Count;

    public void Clear()
    {
        lock (locker)
        {
            list.Clear();
            avails.Clear();
        }
    }

    public virtual void Add(T item)
    {
        lock (locker)
        {
            var who = new Replier
            {
                index = list.Count,
                Item = item,
                isAvail = true
            };

            list.Add(who);
            avails.Enqueue(who.index);
        }
    }

    private Replier Get()
    {
        Replier who = null;

        lock (locker)
        {
            if (Avail > 0)
            {
                who = list[avails.Dequeue()];
                who.isAvail = false;
            }
        }

        return who;
    }

    private void Do(Replier who, Action<T> action)
    {
        action(who.Item);

        lock (locker)
        {
            who.isAvail = true;
            avails.Enqueue(who.index);
        }
    }

    public virtual bool Do(Action<T> action)
    {
        Replier who = Get();
        if (who != null)
        {
            Do(who, action);
            return true;
        }
        return false;
    }

    public virtual Task DoAsync(Action<T> action)
        => Task.Run(() =>
        {
            Replier who = null;
            while (who == null)
            {
                who = Get();
                if (who == null)
                    Thread.Sleep(10);
            }

            Do(who, action);
        });
}
