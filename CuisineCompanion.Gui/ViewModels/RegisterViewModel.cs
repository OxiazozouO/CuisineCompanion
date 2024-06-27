using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Extensions;
using CuisineCompanion.Helper;
using CuisineCompanion.Helpers;
using CuisineCompanion.HttpClients;

namespace CuisineCompanion.ViewModels;

public partial class RegisterViewModel : ObservableValidator
{
    [Required(ErrorMessage = "用户名不能为空")]
    [MinLength(1, ErrorMessage = "用户名长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "用户名长度不能大于{1}")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string name;

    [Required(ErrorMessage = "邮箱不能为空")]
    [RegularExpression(RegexHelper.Email, ErrorMessage = "邮箱格式错误")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string email;

    [Required(ErrorMessage = "手机号不能为空")]
    [RegularExpression(RegexHelper.MobileExact, ErrorMessage = "手机号格式错误")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string phone;

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string password;


    private string confirmPassword;

    [Required(ErrorMessage = "需要输入确认密码")]
    [Compare(nameof(Password), ErrorMessage = "确认密码与密码不匹配")]
    public string ConfirmPassword
    {
        get => confirmPassword;
        set => SetProperty(ref confirmPassword, value, false);
    }

    public RegisterViewModel()
    {
    }

    [RelayCommand]
    private void Register()
    {
        if (MsgBoxHelper.TryError(Error)) return;

        FrontendEncryptionHelper.HasPassword(Password, out var salt, out var hashpswd);

        var req = ApiEndpoints.AccountRegister(new { Name, Email, Phone, Password = hashpswd, Salt = salt });

        if (req.Execute(out var res))
        {
            OnCommandCompleted();
        }
        else
        {
            MsgBoxHelper.TryError(res.Message);
        }
    }

    public event EventHandler CommandCompleted;

    protected virtual void OnCommandCompleted()
    {
        CommandCompleted?.Invoke(this, EventArgs.Empty);
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