namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
///     可能会经常更新的配置
/// </summary>
public class Cfg
{
    /// <summary>
    ///     id
    /// </summary>
    public int CfgId { get; set; }

    /// <summary>
    ///     键
    /// </summary>
    public string CfgK { get; set; } = null!;

    /// <summary>
    ///     值
    /// </summary>
    public string CfgV { get; set; } = null!;
}