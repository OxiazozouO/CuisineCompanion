using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 分类
/// </summary>
public partial class Category
{
    /// <summary>
    /// 分类id
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string CName { get; set; } = null!;
}
