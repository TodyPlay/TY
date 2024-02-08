using TY.Collections;
using TY.Unmanaged;

namespace TY.Entities;

public unsafe struct EntityQuery : IDisposable
{
    /**
     * 查询后匹配的原型
     */
    public UnsafePtrList<Archetype>* matchingTypes;

    public void Dispose()
    {
        matchingTypes->Dispose();
        matchingTypes = default;
    }
}