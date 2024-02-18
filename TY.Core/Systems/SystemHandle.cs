using TY.Unmanaged;

namespace TY.Systems;

public unsafe struct SystemHandle
{
    public void* systemPtr;

    public TypeIndex type;
}