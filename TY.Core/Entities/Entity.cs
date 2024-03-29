﻿using TY.Components;

namespace TY.Entities;

/// <summary>
///     实体实例
/// </summary>
public struct Entity : IQueryTypeParameter
{
    /// <summary>
    /// 实体索引
    /// </summary>
    public int Index;

    /// <summary>
    /// 实体版本
    /// </summary>
    public int Version;
}