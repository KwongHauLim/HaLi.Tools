namespace HaLi.Tools.EasyLock;

public class LockString : LockVar<string>
{
    public LockString() { }
    public LockString(string other) { Value = other; }
    public LockString(char ch, int n) { Value = new string(ch, n); }

    public override int CompareTo(string other) => Value.CompareTo(other);
    public override bool Equals(string other) => Value.Equals(other);

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public static implicit operator LockString(string x) => new LockString(x);
    public static implicit operator string(LockString x) => x.Value;
}
