using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 制作步骤
/// </summary>
public partial class PreparationStep
{
    public int PreparationStepId { get; set; }

    /// <summary>
    /// 食谱id
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// 顺序编号
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 图片路径
    /// </summary>
    public string? FileUri { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Refer { get; set; }

    /// <summary>
    /// 所需时间
    /// </summary>
    public TimeSpan? RequiredTime { get; set; }

    /// <summary>
    /// 所需食材  食材1.食材2.食材3.....|+(+表示加上食材1.食材2.食材3.....).食材a.食材b....|食材制作时间的比例1.....(数量需跟第二个一致)
    /// </summary>
    public string? RequiredIngredient { get; set; }

    /// <summary>
    /// 总结、技巧说明
    /// </summary>
    public string? Summary { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
