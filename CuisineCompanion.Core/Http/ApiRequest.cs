using System.Diagnostics.CodeAnalysis;
using RestSharp;

namespace CuisineCompanion.HttpClients;

/// <summary>
/// 请求模型
/// </summary>
public class ApiRequest
{
    /// <summary>
    /// 请求地址
    /// </summary>
    public string Url { get; init; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public Method Method { get; init; }

    /// <summary>
    /// 请求参数
    /// </summary>
    [NotNull]
    public object Parameters { get; set; }

    /// <summary>
    /// 请求内容类型
    /// </summary>
    public string ContentType { get; set; } = "application/json";
}