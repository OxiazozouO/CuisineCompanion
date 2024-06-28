namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
///     食谱
/// </summary>
public class Recipe
{
    public int RecipeId { get; set; }

    /// <summary>
    ///     创建用户id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    public string? RName { get; set; }

    /// <summary>
    ///     多媒体文件
    /// </summary>
    public string? FileUri { get; set; }

    /// <summary>
    ///     总结
    /// </summary>
    public string? Summary { get; set; }

    public virtual ICollection<PreparationStep> PreparationSteps { get; set; } = new List<PreparationStep>();

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();

    public virtual User User { get; set; } = null!;
}