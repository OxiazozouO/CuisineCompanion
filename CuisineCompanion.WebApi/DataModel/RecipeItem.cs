namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
///     食谱项目
/// </summary>
public class RecipeItem
{
    /// <summary>
    ///     食谱项目id
    /// </summary>
    public int RecipeItemId { get; set; }

    /// <summary>
    ///     食谱id
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    ///     食材id
    /// </summary>
    public int IngredientId { get; set; }

    /// <summary>
    ///     用量
    /// </summary>
    public decimal Dosage { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}