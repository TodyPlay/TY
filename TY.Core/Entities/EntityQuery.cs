using TY.Collections;
using TY.Unmanaged;

namespace TY.Entities;

public unsafe struct EntityQuery
{
    /**
     * 查询后匹配的原型
     */
    public UnsafePtrList<Archetype>* matchingTypes;
}