using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 饮食日记
/// </summary>
public partial class EatingDiary
{
    /// <summary>
    /// id
    /// </summary>
    public int EdId { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 目标类型 0：食材  1：食谱
    /// </summary>
    public sbyte IdCategory { get; set; }

    /// <summary>
    /// 目标id
    /// </summary>
    public int? Tid { get; set; }

    /// <summary>
    /// 用量列表,（包含id和用量）
    /// </summary>
    public string Dosages { get; set; } = null!;

    /// <summary>
    /// 营养元素缓存
    /// </summary>
    public string NutrientContent { get; set; } = null!;

    /// <summary>
    /// 记录的时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    public virtual User User { get; set; } = null!;
}
