using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 分类项目
/// </summary>
public partial class CategoryItem
{
    /// <summary>
    /// 分类项目id
    /// </summary>
    public int CategoryItemId { get; set; }

    /// <summary>
    /// 分类id
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// 目标id
    /// </summary>
    public int TId { get; set; }

    /// <summary>
    /// 目标类型 1：食材 2：食谱 3: 食谱计划
    /// </summary>
    public int IdCategory { get; set; }
}
