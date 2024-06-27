using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 用户身体数据
/// </summary>
public partial class UserPhysicalInfo
{
    /// <summary>
    /// id
    /// </summary>
    public int UpiId { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public int UId { get; set; }

    /// <summary>
    /// 身高
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// 体重(kg)
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// 运动习惯
    /// </summary>
    public string ActivityLevel { get; set; } = null!;

    /// <summary>
    /// 蛋白质摄入量
    /// </summary>
    public double ProteinRequirement { get; set; }

    /// <summary>
    /// 脂肪功能占比
    /// </summary>
    public double FatPercentage { get; set; }

    public DateTime UpdateTime { get; set; }

    public virtual User UIdNavigation { get; set; } = null!;
}
