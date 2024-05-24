namespace HaLi.Tools.Binary;

/// <summary>
/// Read binary from multi stream in order
/// </summary>
public class SequentialStream : Stream
{
    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => ReadLength;
    public override long Position
    {
        get => 0;
        set => Reset();
    }

    public List<Stream> List { get; private set; }
    private Stream pointer;
    private int next = 0;
    private long ReadLength = 0;

    public SequentialStream(IEnumerable<Stream> streams)
    {
        List = new List<Stream>(streams);
        pointer = List.FirstOrDefault();

        foreach (var item in streams)
        {
            ReadLength += item.Length;
        }
    }

    public void Reset()
    {
        pointer = List.FirstOrDefault();
        next = 0;
    }

    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int totalBytesRead = 0;

        while (count > 0 && pointer != null)
        {
            int bytesRead = pointer.Read(buffer, offset, count);
            if (bytesRead == 0)
            {
                pointer.Dispose();
                pointer = List.ElementAtOrDefault(++next);
                offset = 0;
                continue;
            }

            totalBytesRead += bytesRead;
            offset += bytesRead;
            count -= bytesRead;
        }

        return totalBytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return 0;
    }

    public override void SetLength(long value)
    {
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
    }
}
