using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class ChangePasswordModel : ObservableValidator
{
    [Required(ErrorMessage = "必填项")]
    [Range(1000, 9999, ErrorMessage = "请输入4个数字")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private int phoneCut = 1000;

    [ObservableProperty] private string phoneString;

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string password;

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string newPassword;


    private string confirmPassword;

    [Required(ErrorMessage = "需要输入确认密码")]
    [Compare(nameof(NewPassword), ErrorMessage = "确认密码与密码不匹配")]
    public string ConfirmPassword
    {
        get => confirmPassword;
        set => SetProperty(ref confirmPassword, value, false);
    }

    public string Error
    {
        get
        {
            ValidateAllProperties();
            return string.Join('\n', GetErrors());
        }
    }
}