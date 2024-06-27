using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 收藏项目
/// </summary>
public partial class FavoriteItem
{
    /// <summary>
    /// 收藏项目id
    /// </summary>
    public int FavoriteItemId { get; set; }

    /// <summary>
    /// 收藏id
    /// </summary>
    public int FavoriteId { get; set; }

    /// <summary>
    /// 目标id
    /// </summary>
    public int TId { get; set; }
}
