namespace TY.Collections;

public unsafe struct FixedString64
{
    public fixed byte data[64];

    public static implicit operator FixedString64(string source)
    {
        throw new NotImplementedException();
    }

    public static implicit operator string(FixedString64 source)
    {
        throw new NotImplementedException();
    }
}