namespace HaLi.Tools.Hashcode;

public interface IHashCalc
{
    string GetHash(string str);
    string GetHash(int num);
    string GetHash(byte[] binary);
    string GetHash(Stream stream);
}

public class Hash
{
    private static Hash _ptr = null;
    public static Hash Share => _ptr = _ptr ?? new Hash();

    public IHashCalc Algorithm { get; set; }

    private Hash()
    {
        Algorithm = new SHA256();
    }

    public static string GetHash(string str)
        => Share.Algorithm.GetHash(str);

    public static string GetHash(int num)
        => Share.Algorithm.GetHash(num);

    public static string GetHash(byte[] binary)
        => Share.Algorithm.GetHash(binary);

    public static string GetHash(Stream stream)
        => Share.Algorithm.GetHash(stream);
}
