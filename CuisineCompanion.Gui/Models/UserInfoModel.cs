using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class UserInfoModel : ObservableValidator
{
    [Required(ErrorMessage = "活动强度（活动习惯）为必填项")] [NotifyDataErrorInfo] [ObservableProperty]
    private string activityLevelStr;

    /// <summary>
    ///     碳水供能占比  其实只要确定碳水占比就行 碳水化合物50%-65%
    /// </summary>
    [ObservableProperty] private double carbohydratePercentage;

    /// <summary>
    ///     每日碳水化合物摄入量
    /// </summary>
    [ObservableProperty] private double carbohydrateRequirement;

    /// <summary>
    ///     脂肪供能占比 脂肪20%-30%
    /// </summary>
    [Required(ErrorMessage = "脂肪含量为必填项")]
    [Range(0.2, 0.3, ErrorMessage = "脂肪摄入量必须在{1}%~{2}%之间")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private double fatPercentage;

    /// <summary>
    ///     每日脂肪摄入量
    /// </summary>
    [ObservableProperty] private double fatRequirement;

    [Required(ErrorMessage = "身高为必填项")]
    [Range(120, 255, ErrorMessage = "猜你的身高应该在{1}cm和{2}cm之间")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private double height;

    /// <summary>
    ///     蛋白质供能占比 直接按推荐量固定  标准+-5g  占比10%-20%
    /// </summary>
    [ObservableProperty] private double proteinPercentage;

    /// <summary>
    ///     每日蛋白质摄入量
    /// </summary>
    [Required(ErrorMessage = "蛋白质摄入量为必填项")]
    [Range(1, 255, ErrorMessage = "蛋白质摄入量必须在{1}g~{2}g之间")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private double proteinRequirement;

    /// <summary>
    ///     蛋白质推荐量  来自2023中国居民膳食营养素参考摄入量 附表3-2 膳食蛋白质参考摄入量
    /// </summary>
    [ObservableProperty] private double proteinRni;

    [ObservableProperty] private DateTime updateTime;
    [ObservableProperty] private int upiId;

    [Required(ErrorMessage = "体重为必填项")]
    [Range(30, 255, ErrorMessage = "猜你的体重比{1}kg 重, 比{2}kg 轻")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private double weight;


    public string Error
    {
        get
        {
            ValidateAllProperties();
            return string.Join('\n', GetErrors());
        }
    }
}