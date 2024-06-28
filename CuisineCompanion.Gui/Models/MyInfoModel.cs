using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Helper;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Models;

public partial class MyInfoModel : ObservableValidator
{
    [Required(ErrorMessage = "生日为必填项")] [NotifyDataErrorInfo] [ObservableProperty]
    private DateTime birthDate;

    [Required(ErrorMessage = "性别为必填项")] [NotifyDataErrorInfo] [ObservableProperty]
    private bool gender;

    [ObservableProperty] private int userId;
    [ObservableProperty] private ObservableCollection<UserInfoModel> userInfoList;

    [Required(ErrorMessage = "昵称为必填项")]
    [StringLength(20, ErrorMessage = "长度必须比{1}短")]
    [Display(Name = "用户名")]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    private string userName;

    [ObservableProperty] private UserInfoModel userNowInfo;

    public string Error
    {
        get
        {
            ValidateAllProperties();
            return string.Join('\n', GetErrors());
        }
    }


    public void Init()
    {
        try
        {
            if (UserNowInfo is null) return;
            Bmi = NutritionalHelper.GetBmi(UserNowInfo.Weight, UserNowInfo.Height);
            BmiStr = NutritionalHelper.GetBmiString(Bmi);

            Ree = NutritionalHelper.GetMifflinStJeorREE(UserNowInfo.Weight, UserNowInfo.Height, BirthDate, Gender);
            MainViewModel.ActivityLevels.TryGetValue(UserNowInfo.ActivityLevelStr ?? "", out var activityLevel);
            Tdee = NutritionalHelper.TDEE(Ree, activityLevel);
            MaxTdee = Tdee * 1.2;

            UserNowInfo.ProteinRni = MainViewModel.ProteinRequirement[Gender][BirthDate.GetAge()];
            if (UserNowInfo.ProteinPercentage == 0)
                UserNowInfo.ProteinPercentage = Math.Min(Math.Max(0.10, UserNowInfo.ProteinRni * 4 / Tdee), 0.20);
            else
                UserNowInfo.ProteinPercentage = Math.Min(Math.Max(0.10, UserNowInfo.ProteinPercentage), 0.20);

            UserNowInfo.ProteinRequirement = (double)((decimal)UserNowInfo.ProteinPercentage * (decimal)Tdee / 4);

            // a + b = c;
            // 0.5<a<0.65
            // 0.2<b<0.3
            //控制 a 和 b 在合理范围内
            var a = UserNowInfo.FatPercentage;
            var c = 1 - UserNowInfo.ProteinPercentage;

            a = c switch
            {
                >= 0.8 and <= 0.85 => Math.Min(Math.Max(0.2, a), 0.3),
                > 0.85 and <= 0.90 => Math.Min(Math.Max(c - 0.65, a), 0.3),
                _ => a
            };

            UserNowInfo.CarbohydratePercentage = c - a;
            UserNowInfo.FatPercentage = a;

            UserNowInfo.CarbohydrateRequirement = UserNowInfo.CarbohydratePercentage * Tdee / 4;
            UserNowInfo.FatRequirement = UserNowInfo.FatPercentage * Tdee / 9;
        }
        catch
        {
            // ignored
        }
    }

    partial void OnUserInfoListChanged(ObservableCollection<UserInfoModel>? oldValue,
        ObservableCollection<UserInfoModel> newValue)
    {
        UserInfoList.CollectionChanged += UserInfoListOnCollectionChanged;
        if (UserInfoList.Count > 0) GetNowInfo();
    }

    private void UserInfoListOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        GetNowInfo();
    }

    partial void OnUserNowInfoChanged(UserInfoModel? oldValue, UserInfoModel newValue)
    {
        if (UserNowInfo is null)
            Init();
        else
            UserNowInfo.PropertyChanged += UserNowInfoOnPropertyChanged;
    }

    private void UserNowInfoOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(UserInfoModel.ProteinPercentage):
                Init();
                break;
            case nameof(UserInfoModel.FatPercentage):
                Init();
                break;
            case nameof(UserInfoModel.Weight):
                Init();
                break;
            case nameof(UserInfoModel.Height):
                Init();
                break;
            case nameof(UserInfoModel.ActivityLevelStr):
                Init();
                break;
        }
    }

    partial void OnBirthDateChanged(DateTime oldValue, DateTime newValue)
    {
        Init();
    }

    partial void OnGenderChanged(bool oldValue, bool newValue)
    {
        Init();
    }

    public void GetNowInfo()
    {
        if (UserInfoList is not null)
            UserNowInfo = UserInfoList?.OrderByDescending(user => user.UpdateTime)
                .OrderBy(user => Math.Abs((user.UpdateTime - DateTime.Now).TotalSeconds))
                .FirstOrDefault() ?? new UserInfoModel();
    }

    #region 辅助变量

    [ObservableProperty] private double bmi;
    [ObservableProperty] private string bmiStr;

    /// <summary>
    ///     静息代谢
    /// </summary>
    [ObservableProperty] private double ree;

    /// <summary>
    ///     基础代谢
    /// </summary>
    [ObservableProperty] private double tdee;

    [ObservableProperty] private double maxTdee;

    #endregion
}