namespace HaLi.Tools.EasyLock
{
    public class LockINT : LockVar<int>
    {
        public LockINT() { }
        public LockINT(int x) { Value = x; }

        public override int CompareTo(int other) => Value.CompareTo(other);
        public override bool Equals(int other) => Value.Equals(other);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public static implicit operator LockINT(int x) => new LockINT(x);
        public static implicit operator int(LockINT x) => x.Value;
        public static int operator +(LockINT x, int y) => x.Value + y;
        public static int operator -(LockINT x, int y) => x.Value - y;
        public static int operator *(LockINT x, int y) => x.Value * y;
        public static int operator /(LockINT x, int y) => x.Value / y;
        public static int operator %(LockINT x, int y) => x.Value % y;
        public static LockINT operator ++(LockINT x) { x.Value++; return x; }
        public static LockINT operator --(LockINT x) { x.Value--; return x; }

        public static int operator ~(LockINT x) => ~x.Value;
        public static int operator &(LockINT x, int y) => x.Value & y;
        public static int operator |(LockINT x, int y) => x.Value | y;
        public static int operator ^(LockINT x, int y) => x.Value ^ y;
        public static int operator <<(LockINT x, int n) => x.Value << n;
        public static int operator >>(LockINT x, int n) => x.Value >> n;

        public static bool operator ==(LockINT x, int y) => x.Value == y;
        public static bool operator !=(LockINT x, int y) => x.Value != y;
        public static bool operator >=(LockINT x, int y) => x.Value >= y;
        public static bool operator <=(LockINT x, int y) => x.Value <= y;
        public static bool operator >(LockINT x, int y) => x.Value > y;
        public static bool operator <(LockINT x, int y) => x.Value < y;
    }
}
