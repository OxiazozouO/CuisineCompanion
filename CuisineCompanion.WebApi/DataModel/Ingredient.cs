using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 食材
/// </summary>
public partial class Ingredient
{
    /// <summary>
    /// 食材id
    /// </summary>
    public int IngredientId { get; set; }

    /// <summary>
    /// 创建用户id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string IName { get; set; } = null!;

    /// <summary>
    /// 多媒体文件
    /// </summary>
    public string? FileUri { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Refer { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// 量
    /// </summary>
    public string? Quantity { get; set; }

    /// <summary>
    /// 营养成分 默认净含量100g来计算
    /// </summary>
    public string? NutritionalComposition { get; set; }

    /// <summary>
    /// 过敏信息
    /// </summary>
    public string? Allergy { get; set; }

    /// <summary>
    /// 净含量
    /// </summary>
    public double Content { get; set; }

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();

    public virtual User User { get; set; } = null!;
}
