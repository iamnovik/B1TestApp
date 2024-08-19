using System.Threading;

namespace B1TestApp.Utilities;

public class AtomicLong
{
    private long _value;
    public long Value
    {
        get => Interlocked.Read(ref _value);
        set => Interlocked.Exchange(ref _value, value);
    }

    public AtomicLong(long initialValue = 0)
    {
        _value = initialValue;
    }
}