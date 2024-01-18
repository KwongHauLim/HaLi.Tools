namespace HaLi.Tools.EasyLock;

public class LockFloat : LockVar<float>
{
    public LockFloat() { }
    public LockFloat(float x) { Value = x; }

    public override int CompareTo(float other) => Value.CompareTo(other);
    public override bool Equals(float other) => Value.Equals(other);

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public static implicit operator LockFloat(float x) => new LockFloat(x);
    public static implicit operator float(LockFloat x) => x.Value;
    public static float operator +(LockFloat x, float y) => x.Value + y;
    public static float operator -(LockFloat x, float y) => x.Value - y;
    public static float operator *(LockFloat x, float y) => x.Value * y;
    public static float operator /(LockFloat x, float y) => x.Value / y;
    public static float operator %(LockFloat x, float y) => x.Value % y;

    public static bool operator ==(LockFloat x, float y) => x.Value.CompareTo(y) == 0;
    public static bool operator !=(LockFloat x, float y) => x.Value.CompareTo(y) != 0;
    public static bool operator >=(LockFloat x, float y) => x.Value.CompareTo(y) >= 0;
    public static bool operator <=(LockFloat x, float y) => x.Value.CompareTo(y) <= 0;
    public static bool operator >(LockFloat x, float y) => x.Value.CompareTo(y) > 0;
    public static bool operator <(LockFloat x, float y) => x.Value.CompareTo(y) < 0;
}
