using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 收藏
/// </summary>
public partial class Favorite
{
    /// <summary>
    /// 收藏id
    /// </summary>
    public int FavoriteId { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 多媒体文件
    /// </summary>
    public string FileUri { get; set; } = null!;

    /// <summary>
    /// 名称
    /// </summary>
    public string CName { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Refer { get; set; }

    /// <summary>
    /// 权限 1：私有收藏夹 2：公开菜单收藏夹
    /// </summary>
    public sbyte Authority { get; set; }

    /// <summary>
    /// 收藏类型 1：食材 2：食谱 3：菜单
    /// </summary>
    public sbyte IdCategory { get; set; }

    public virtual User User { get; set; } = null!;
}
