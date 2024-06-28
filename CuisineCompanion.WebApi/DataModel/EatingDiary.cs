namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
///     食用日记
/// </summary>
public class EatingDiary
{
    /// <summary>
    ///     id
    /// </summary>
    public int EdId { get; set; }

    public int UserId { get; set; }

    /// <summary>
    ///     0：食材  1：食谱
    /// </summary>
    public sbyte IdCategory { get; set; }

    /// <summary>
    ///     目标id
    /// </summary>
    public int? Tid { get; set; }

    /// <summary>
    ///     用量列表,（包含id和用量）
    /// </summary>
    public string Dosages { get; set; } = null!;

    /// <summary>
    ///     简短的营养元素表 （能量、碳水、蛋白质、脂肪）
    /// </summary>
    public string ShortNutrientContent { get; set; } = null!;

    /// <summary>
    ///     记录的时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    public virtual User User { get; set; } = null!;
}